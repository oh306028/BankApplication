using BankApplication.App.Exceptions;
using BankApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BankApplication.App.Modules.Account.Models.Create;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.App.Modules.Client.Models.Create;
using AutoMapper;
using BankApplication.Data.Entities;
using System.Security.Principal;
using BankApplication.App.Services.Client;

namespace BankApplication.App.Services.Account
{
    public interface IAccountService
    {
        Task<SuccededLoginModel> Login(LoginModel model);

        Task<string> VerifyCode(VerifyCodeModel model);
        void Register(RegisterModel model);
        bool IsAdmin(int userId);

        List<ClientDetails> GetAdmins();
    }

    public class AccountService : IAccountService
    {
        private readonly AppDbContext context;
        private readonly JwtOptions authenticationOptions;
        private readonly IMapper mapper;
        private readonly IEmailService emails;

        public AccountService(AppDbContext context, JwtOptions authenticationOptions, IMapper mapper, IEmailService emails)   
        {
            this.context = context;
            this.authenticationOptions = authenticationOptions;
            this.mapper = mapper;
            this.emails = emails;
        }
        public void Register(RegisterModel model)
        {
            var client = context.Clients.FirstOrDefault(e => e.Email.ToLower() == model.Email.ToLower());

            if (client is null)
                throw new NotFoundException("Nie znaleziono klienta lub niepoprawny kod klienta");

            if (!client.IsActive)   
                throw new NotActiveClientException("Klient nie jest aktywny");

            if (client.ClientCode != model.ClientCode)
                throw new NotFoundException("Nie znaleziono klienta lub niepoprawny kod klienta");

            var passwordHasher = new PasswordHasher<BankApplication.Data.Entities.Client>();
            var passwordHash = passwordHasher.HashPassword(client, model.Password);

            var account = new BankApplication.Data.Entities.Account()
            {
                ClientId = client.Id,
                IsBlocked = false,
                Login = model.Login,
                PasswordHash = passwordHash,
                IsDoubleAuthenticated = false,
                IsEmployee = false
            };

            context.Accounts.Add(account);
            context.SaveChanges();

        }

        public async Task<SuccededLoginModel> Login(LoginModel model)
        {
            var account = await context.Accounts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(l => l.Login == model.Login);

            if (account is null)
                throw new NotFoundException("Błędny login lub hasło");

            await EnsureAccountNotBlocked(account.Id);

            var passwordHasher = new PasswordHasher<BankApplication.Data.Entities.Account>();
            var result = passwordHasher.VerifyHashedPassword(account, account.PasswordHash, model.Password);

            var logging = new Logging(account.Id);


            if (result == PasswordVerificationResult.Failed)
            {
               logging.IsSuccess = false;
                context.Loggings.Add(logging);
                await context.SaveChangesAsync();

                await TryApplyLoginBlockade(account.Id);
                throw new NotFoundException("Błędny login lub hasło. Konto zostanie zablokowane po 3 nieudanych próbach.");
            }
            else
            {
                if (IsAdmin(account.Id))
                {
                    logging.LoginCode = "1234567";           
                }
                else
                {
                    logging.LoginCode = GenerateLoginCode();
                    await emails.SendVerificationEmailAsync(account.Client.Email, logging.LoginCode);
                }
                  
                context.Loggings.Add(logging);
                await context.SaveChangesAsync();
            }

           

            return new SuccededLoginModel()
            {
                LoginAttemptId = logging.Id
            };

        }

        public async Task<string> VerifyCode(VerifyCodeModel model) 
        {
            var logging = await context.Loggings
                                        .Include(l => l.Account)
                                        .ThenInclude(p => p.Client)
                                        .SingleOrDefaultAsync(l => l.Id == model.LoginAttemptId);

            if (logging.LoginCode != model.VerificationCode)
                throw new NotFoundException("Błędny kod weryfikacyjny");

            logging.IsSuccess = true;
            await context.SaveChangesAsync();

            var token = GenerateToken(logging.Account);
            return token;
        }


        private static string GenerateLoginCode()   
        {
            var availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()/".ToCharArray();

            var builder = new StringBuilder(6);
            var Random = new Random();

            for (int i = 0; i <= 6; i++)
            {
                var index = Random.Next(availableChars.Length);
                builder.Append(availableChars[index]);
            }

            return builder.ToString();
        }


        private async Task EnsureAccountNotBlocked(int accountId)
        {
            var activeBlockade = await context.Blockades
                .FirstOrDefaultAsync(b => b.AccountId == accountId && b.IsActive);

            if (activeBlockade != null)
            {
                if (activeBlockade.BlockDateTo.HasValue && activeBlockade.BlockDateTo <= DateTime.UtcNow)
                {
                    activeBlockade.IsActive = false;
                    context.Blockades.Update(activeBlockade);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new UnauthorizedAccessException("Konto zostało zablokowane z powodu wielu nieudanych prób logowania. Odczekaj 15 minut.");
                }
            }
        }

        private async Task TryApplyLoginBlockade(int accountId)
        {
            var recentAttempts = await context.Loggings
                .Where(l => l.AccountId == accountId)
                .OrderByDescending(l => l.LogInDate)
                .Take(3)
                .ToListAsync();

            if (recentAttempts.Count == 3 && recentAttempts.All(l => !l.IsSuccess))
            {
                var blockade = new Blockade
                {
                    AccountId = accountId,
                    Reason = "3 błędne próby logowania",
                    BlockDateFrom = DateTime.UtcNow,
                    BlockDateTo = DateTime.UtcNow.AddMinutes(15),
                    IsActive = true
                };

                context.Blockades.Add(blockade);
                await context.SaveChangesAsync();
            }
        }



        public bool IsAdmin(int userId)
        {
            var account = context.Accounts  
                .FirstOrDefault(p => p.Id == userId);

            if (account == null)
                throw new NotFoundException("Nie znaleziono użytkownika");

            return account.IsEmployee;
        }


        private string GenerateToken(BankApplication.Data.Entities.Account account)     
        {
            var key = Encoding.ASCII.GetBytes(authenticationOptions.Key);
            var securityKey = new SymmetricSecurityKey(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, account.Client.Email),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())

            }),
                Expires = DateTime.UtcNow.AddDays(3),
                Issuer = authenticationOptions.Issuer,
                Audience = authenticationOptions.Audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public List<ClientDetails> GetAdmins()
        {

            var clients = context.Clients
                .Include(p => p.Account)
                .Where(p => p.Account.IsEmployee == true && p.IsActive == true)
                .AsNoTracking().ToList();
            var result = mapper.Map<List<ClientDetails>>(clients);

            return result;
        }
    }
}

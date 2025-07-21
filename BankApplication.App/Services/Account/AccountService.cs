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

namespace BankApplication.App.Services.Account
{
    public interface IAccountService
    {
        string Login(LoginModel model);
        void Register(RegisterModel model);
        bool IsAdmin(int userId);
    }

    public class AccountService : IAccountService
    {
        private readonly AppDbContext context;
        private readonly JwtOptions authenticationOptions;
        private readonly IMapper mapper;

        public AccountService(AppDbContext context, JwtOptions authenticationOptions, IMapper mapper)   
        {
            this.context = context;
            this.authenticationOptions = authenticationOptions;
            this.mapper = mapper;
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

        public string Login(LoginModel model)
        {
            var client = context.Clients
                .FirstOrDefault(e => e.Email.ToLower() == model.Email.ToLower());

            var account = context.Accounts
                                .Include(c => c.Client)
                                .FirstOrDefault(l => l.Login == model.Login);

            if (client is null)
                throw new NotFoundException("Nie znaleziono klienta");

            if(account is null)
                throw new NotFoundException("Błędny login lub hasło");

            var passwordHasher = new PasswordHasher<BankApplication.Data.Entities.Account>();
            var result = passwordHasher.VerifyHashedPassword(account, account.PasswordHash, model.Password);

            var loggin = new Logging(account.Id);

            if (result == PasswordVerificationResult.Failed)
            {
                loggin.IsSuccess = false;
                throw new NotFoundException("Błędny login lub hasło");
            }

            loggin.IsSuccess = true;
            
            var token = GenerateToken(account);
            context.SaveChanges();

            return token;
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
      
    }
}

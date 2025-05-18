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

            if (result == PasswordVerificationResult.Failed)
                throw new NotFoundException("Błędny login lub hasło");

            var token = GenerateToken(account);
            return token;
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

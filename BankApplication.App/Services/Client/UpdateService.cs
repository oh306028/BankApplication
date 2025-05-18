using AutoMapper;
using BankApplication.App.Exceptions;
using BankApplication.App.Modules.Account.Models.Create;
using BankApplication.App.Modules.Client.Models.Create;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankApplication.App.Services.Client
{
    public interface IUpdateService
    {
        Data.Entities.Client Join(ClientForm model);
        void Update(ClientForm model);
    }

    public class UpdateService : IUpdateService
    {
        private readonly AppDbContext context;
        private readonly JwtOptions authenticationOptions;
        private readonly IMapper mapper;

        public UpdateService(AppDbContext context, JwtOptions authenticationOptions, IMapper mapper)
        {
            this.context = context;
            this.authenticationOptions = authenticationOptions;
            this.mapper = mapper;
        }
        public BankApplication.Data.Entities.Client Join(ClientForm model)
        {
            var newClient = mapper.Map<BankApplication.Data.Entities.Client>(model);
            context.Clients.Add(newClient);
            context.SaveChanges();

            return newClient;
        }

        public void Update(ClientForm model)
        {
            var newClient = mapper.Map<BankApplication.Data.Entities.Client>(model);
            context.Clients.Update(newClient);
            context.SaveChanges();

        }

    }
}

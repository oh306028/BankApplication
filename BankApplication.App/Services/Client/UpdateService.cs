using AutoMapper;
using BankApplication.App.Exceptions;
using BankApplication.App.Modules.Account.Models.Create;
using BankApplication.App.Modules.BankAccount.Models.Update;
using BankApplication.App.Modules.Client.Models.Create;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.Data;
using BankApplication.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BankApplication.App.Services.Client
{
    public interface IUpdateService
    {
        Data.Entities.Client Join(ClientForm model);
        void Update(ClientForm model);
        void ManageClientRequest(Guid clientId, BlockRequestModel model, int adminId);
    }

    public class UpdateService : IUpdateService
    {
        private readonly AppDbContext context;
        private readonly JwtOptions authenticationOptions;
        private readonly IMapper mapper;
        private readonly IEmailService emails;

        public UpdateService(AppDbContext context, JwtOptions authenticationOptions, IMapper mapper, IEmailService emails)
        {
            this.context = context;
            this.authenticationOptions = authenticationOptions;
            this.mapper = mapper;
            this.emails = emails;
        }
        public BankApplication.Data.Entities.Client Join(ClientForm model)
        {
            var newMail = context.Clients.FirstOrDefault(e => e.Email.ToLower() == model.Email.ToLower());
            if (newMail != null)
                throw new NotFoundException("Ten adres email jest już wykorzystany");

            var newClient = mapper.Map<BankApplication.Data.Entities.Client>(model);
            newClient.IsActive = false; //admin has to accept new client
            newClient.ClientCode = GenerateRandomString();
            newClient.PublicId = Guid.NewGuid();

            context.Clients.Add(newClient);
            context.SaveChanges();

            var newClientRequest = new BecomeClientRequest()
            {
                ClientId = newClient.Id,
                RequestDate = DateTime.Now,
                IsAccepted = false,
                IsActive = true,
            };

            context.BecomeClientRequests.Add(newClientRequest);
            context.SaveChanges();


            return newClient;
        }

        public void ManageClientRequest(Guid clientId, BlockRequestModel model, int adminId)
        {
            var client = context.Clients.Single(p => p.PublicId == clientId);

            var clientRequest = context.BecomeClientRequests.Single(p => p.PublicId == model.PublicId);

            clientRequest.AdminId = adminId;
            clientRequest.IsAccepted = model.Accepted;
            clientRequest.ManagedDate = DateTime.Now;
            clientRequest.IsActive = false;

            client.IsActive = model.Accepted;

            context.SaveChanges();

            emails.SendClientCodeMail(client.Email, client.ClientCode);
        }

        private static string GenerateRandomString()
        {
            var availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            var builder = new StringBuilder(12);
            var Random = new Random();

            for (int i = 0; i <= 12; i++)
            {
                var index = Random.Next(availableChars.Length);
                builder.Append(availableChars[index]);
            }

            return builder.ToString();
        }

        public void Update(ClientForm model)
        {
            var newClient = mapper.Map<BankApplication.Data.Entities.Client>(model);
            context.Clients.Update(newClient);
            context.SaveChanges();

        }

    }
}

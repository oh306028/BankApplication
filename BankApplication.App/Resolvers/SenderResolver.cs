using AutoMapper;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.Data;
using BankApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.App.Resolvers
{
    public class SenderResolver : IValueResolver<Transfer, TransferDetails, string>
    {
        public SenderResolver(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }
        public IHttpContextAccessor ContextAccesor { get; }

        public string Resolve(Transfer source, TransferDetails destination, string destMember, ResolutionContext context)
        {
           var bankAccount = Context.BankAccounts
                .Include(p => p.Client)
                .Single(p => p.Id == source.AccountFromId);

            var client = bankAccount.Client;
            return client.FullName;

        }
    }
}

using AutoMapper;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.Data.Entities;
using BankApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.App.Resolvers
{
    public class ReceiverResolver : IValueResolver<Transfer, TransferDetails, string>
    {
        public ReceiverResolver(AppDbContext context)
        {
            Context = context; 
        }

        public AppDbContext Context { get; }
        public IHttpContextAccessor ContextAccesor { get; }

        public string Resolve(Transfer source, TransferDetails destination, string destMember, ResolutionContext context)
        {
            var bankAccount = Context.BankAccounts
                 .Include(p => p.Client)
                 .Single(p => p.Id == source.AccountToId);

            var client = bankAccount.Client;
            return client.FullName;

        }
    }
}

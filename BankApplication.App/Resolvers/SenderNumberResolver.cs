using AutoMapper;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.Data.Entities;
using BankApplication.Data;

namespace BankApplication.App.Resolvers
{
    public class SenderNumberResolver : IValueResolver<Transfer, TransferDetails, string>
    {
        public SenderNumberResolver(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }
        public IHttpContextAccessor ContextAccesor { get; }

        public string Resolve(Transfer source, TransferDetails destination, string destMember, ResolutionContext context)
        {
            var bankAccount = Context.BankAccounts
                 .Single(p => p.Id == source.AccountFromId);    

            return bankAccount.BankAccountNumber;

        }
    }
}

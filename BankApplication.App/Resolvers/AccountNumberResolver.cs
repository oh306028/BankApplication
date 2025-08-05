using AutoMapper;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.Data;
using BankApplication.Data.Entities;

namespace BankApplication.App.Resolvers
{
    public class AccountNumberResolver : IValueResolver<BankAccountBlockRequest, BlockRequestDetails, string>
    {
        public AccountNumberResolver(AppDbContext context)
        {
            Context = context;  
        }

        public AppDbContext Context { get; }
        public string Resolve(BankAccountBlockRequest source, BlockRequestDetails destination, string destMember, ResolutionContext context)
        {
            var account = Context.BankAccounts
                  .Single(p => p.Id == source.BankAccountId);

            return account.BankAccountNumber;
        }
    }
}

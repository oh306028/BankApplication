using AutoMapper;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.Data.Entities;
using BankApplication.Data;

namespace BankApplication.App.Resolvers
{
    public class AccountIdResolver : IValueResolver<BankAccountBlockRequest, BlockRequestDetails, string>
    {
        public AccountIdResolver(AppDbContext context)
        {
            Context = context;
        }   

        public AppDbContext Context { get; }
        public string Resolve(BankAccountBlockRequest source, BlockRequestDetails destination, string destMember, ResolutionContext context)
        {
            var account = Context.BankAccounts
                  .Single(p => p.Id == source.BankAccountId);

            return account.PublicId.ToString();
        }
    }
}

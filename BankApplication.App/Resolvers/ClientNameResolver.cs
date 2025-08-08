using AutoMapper;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.Data.Entities;
using BankApplication.Data;
using BankApplication.App.Modules.Client.Models.Details;

namespace BankApplication.App.Resolvers
{
    public class ClientNameResolver : IValueResolver<Logging, LogginDetails, string>,
                                      IValueResolver<BankAccountBlockRequest, BlockRequestDetails, string>,
                                      IValueResolver<BecomeClientRequest, ClientRequestDetails, string>
    {
        public ClientNameResolver(AppDbContext context) 
        {   
            Context = context;      
        }

        public AppDbContext Context { get; }

        public string Resolve(Logging source, LogginDetails destination, string destMember, ResolutionContext context)
        {
            var account = Context.Accounts  
                 .Single(p => p.Id == source.AccountId);

            return account.Client.FullName;

        }

        public string Resolve(BankAccountBlockRequest source, BlockRequestDetails destination, string destMember, ResolutionContext context)
        {
            var account = Context.BankAccounts
                 .Single(p => p.Id == source.BankAccountId);

            return account.Client.FullName;
        }

        public string Resolve(BecomeClientRequest source, ClientRequestDetails destination, string destMember, ResolutionContext context)
        {
            var client = Context.Clients.Single(p => p.Id == source.ClientId);
            return client.FullName;
        }
    }
}

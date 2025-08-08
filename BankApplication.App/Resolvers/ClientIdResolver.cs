using AutoMapper;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.Data.Entities;
using BankApplication.Data;

namespace BankApplication.App.Resolvers
{
    public class ClientIdResolver : IValueResolver<BecomeClientRequest, ClientRequestDetails, string>
    {
        public ClientIdResolver(AppDbContext context)
        {   
            Context = context;
        }

        public AppDbContext Context { get; }
        public string Resolve(BecomeClientRequest source, ClientRequestDetails destination, string destMember, ResolutionContext context)
        {
            var client = Context.Clients    
                  .Single(p => p.Id == source.ClientId);   

            return client.PublicId.ToString();
        }
    }
}

using AutoMapper;
using BankApplication.App.Exceptions;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.Data;
using BankApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.App.Services.Client
{
    public interface IDetailService
    {
        ClientDetails Fetch(Guid id);
        ClientDetails Fetch(int id);

        List<ClientDetails> List();
        List<LogginDetails> LoginAttempts();

        List<BlockRequestDetails> BlockRequests();
        List<ClientRequestDetails> ClientRequests();    
    }

    public class DetailService : IDetailService
    {
        private readonly AppDbContext context;
        private readonly JwtOptions authenticationOptions;
        private readonly IMapper mapper;

        public DetailService(AppDbContext context, JwtOptions authenticationOptions, IMapper mapper)
        {
            this.context = context;
            this.authenticationOptions = authenticationOptions;
            this.mapper = mapper;
        }

        public List<BlockRequestDetails> BlockRequests()
        {
            var blockades = context.BankAccountBlockRequests
                .Include(p => p.BankAccount).ThenInclude(p => p.Client)
                .ToList();

            return mapper.Map<List<BlockRequestDetails>>(blockades);

        }

        public List<ClientRequestDetails> ClientRequests()    
        {
            var blockades = context.BecomeClientRequests
                .Include(p => p.Client)
                .ToList();

            return mapper.Map<List<ClientRequestDetails>>(blockades);

        }

        public ClientDetails Fetch(Guid id)
        {
            var client = context.Clients.FirstOrDefault(i => i.PublicId == id);
            if (client is null)
                throw new NotFoundException("Nie znaleziono klienta");

            var result = mapper.Map<ClientDetails>(client);

            return result;
        }

        public ClientDetails Fetch(int id)
        {
            var client = context.Clients.FirstOrDefault(i => i.Id == id);
            if (client is null)
                throw new NotFoundException("Nie znaleziono klienta");

            var result = mapper.Map<ClientDetails>(client);

            return result;
        }

        public List<ClientDetails> List()
        {
            var clients = context.Clients    
                .Include(p => p.Account)
                .Where(p => p.Account.IsEmployee == false && p.IsActive == true)
                .AsNoTracking().ToList();
            var result = mapper.Map<List<ClientDetails>>(clients);

            return result;
        }

        public List<LogginDetails> LoginAttempts()  
        {
            var loggins = context.Loggings.Include(p => p.Account).ThenInclude(p => p.Client).ToList();
            return mapper.Map<List<LogginDetails>>(loggins);
        }
    }
}

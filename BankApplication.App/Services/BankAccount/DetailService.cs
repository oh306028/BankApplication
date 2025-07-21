using AutoMapper;
using BankApplication.Data;
using BankApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.App.Services.BankAccount
{
    public interface IDetailService
    {
       List<Data.Entities.BankAccount> Fetch(int id);     
    }

    public class DetailService : IDetailService
    {   
        private readonly AppDbContext context;

        public DetailService(AppDbContext context)
        {
            this.context = context;
        }
        public List<BankApplication.Data.Entities.BankAccount> Fetch(int id)  
        {
            var account = context.Accounts
                .Include(p => p.Client)
                .ThenInclude(p => p.BankAccounts)
                .FirstOrDefault(i => i.Id == id);     

            return account.Client.BankAccounts;
        }
    }
}

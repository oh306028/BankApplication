using AutoMapper;
using BankApplication.App.Exceptions;
using BankApplication.App.Modules.BankAccount.Models.Update;
using BankApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.App.Services.BankAccount
{
    public interface IUpdateService
    {
        void Create(int userId, CreateBankAccountModel model);
    }

    public class UpdateService : IUpdateService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public UpdateService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void Create(int userId, CreateBankAccountModel model)
        {
            var account = context.Accounts
               .Include(p => p.Client)
               .FirstOrDefault(i => i.Id == userId);

            if (account == null)
                throw new NotFoundException("Nie znaleziono klienta");

            var bankAccount = new BankApplication.Data.Entities.BankAccount();
            bankAccount.Type = (int)model.Type;
           
            if(!string.IsNullOrEmpty(model.Currency))
                bankAccount.Currency = model.Currency;

            if(model.InteresetRate.HasValue)
                bankAccount.InteresetRate = model.InteresetRate;
     
            bankAccount.ClientId = account.ClientId;
            bankAccount.Amount = 0;

            context.BankAccounts.Add(bankAccount);
            context.SaveChanges();

        }

    }
}

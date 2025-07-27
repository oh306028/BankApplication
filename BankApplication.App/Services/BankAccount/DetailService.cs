using AutoMapper;
using BankApplication.App.Exceptions;
using BankApplication.Data;
using BankApplication.Data.Entities;
using BankApplication.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.App.Services.BankAccount
{
    public interface IDetailService
    {
       List<Data.Entities.BankAccount> Fetch(int id);
       List<int> GetOwnTypes(int id);
       BankApplication.Data.Entities.BankAccount GetDetailsByType(int userId, BankAccountType type);
       List<Transfer> GetTransfersSent(Guid publicId);
        public List<Transfer> GetTransfersReceived(Guid publicId);
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

        public BankApplication.Data.Entities.BankAccount GetDetailsByType(int userId, BankAccountType type)
        {
            var account = context.Accounts
             .Include(p => p.Client)
             .ThenInclude(p => p.BankAccounts)
             .FirstOrDefault(i => i.Id == userId);

            var result = account.Client.BankAccounts.SingleOrDefault(p => p.Type == (int)type);

            if (result == null)
                throw new NotFoundException("Nie znaleziono podanego typu konta");

            return result;

        }

        public int GetAccountIdForTransfer(string accountNumber)
        {
            var account = context.BankAccounts
                .Single(p => p.BankAccountNumber == accountNumber);

            return account.Id;
        }

        public List<Transfer> GetTransfersSent(Guid publicId)
        {
            var bankAccount = context.BankAccounts
                .Include(p => p.TransfersSent)
                .FirstOrDefault(p => p.PublicId == publicId);

            if (bankAccount == null)
                throw new NotFoundException("Nie znaleziono konta bankowego"); 

            return bankAccount.TransfersSent;      
        }

        public List<Transfer> GetTransfersReceived(Guid publicId) 
        {
            var bankAccount = context.BankAccounts
               .Include(p => p.TransfersReceived)   
               .FirstOrDefault(p => p.PublicId == publicId);

            if (bankAccount == null)
                throw new NotFoundException("Nie znaleziono konta bankowego");

            return bankAccount.TransfersReceived;
        }

        public List<int> GetOwnTypes(int id)
        {
            var account = context.Accounts
                .Include(p => p.Client)
                .ThenInclude(p => p.BankAccounts)
                .FirstOrDefault(i => i.Id == id);

            var list = new List<int>();
            account.Client.BankAccounts.ForEach(p => list.Add(p.Type));

            return list;
        }
    }
}

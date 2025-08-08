using AutoMapper;
using BankApplication.App.Exceptions;
using BankApplication.App.Modules.BankAccount.Models.Details;
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
        List<Transfer> GetList(Guid publicId);
        List<Transfer> GetAll();

        List<Data.Entities.BankAccount> GetAllAccounts();
        bool HasActiveBlockRequests(Guid accountId);

        bool IsBlocked(Guid accountId);


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

            return bankAccount.TransfersSent.OrderByDescending(p => p.TransferDate).ToList();      
        }

        public List<Transfer> GetList(Guid publicId)
        {
            var bankAccount = context.BankAccounts
                .Include(p => p.TransfersSent)
                .Include(p => p.TransfersReceived)
                .FirstOrDefault(p => p.PublicId == publicId);

            if (bankAccount == null)
                throw new NotFoundException("Nie znaleziono konta bankowego");

            var list = new List<Transfer>();
            list.AddRange(bankAccount.TransfersSent);
            list.AddRange(bankAccount.TransfersReceived);

            return list.OrderByDescending(p => p.TransferDate).ToList();
        }

        public List<Transfer> GetAll() => context.Transfers.AsNoTracking().OrderByDescending(p => p.TransferDate).ToList();

           
        public List<Transfer> GetTransfersReceived(Guid publicId) 
        {
            var bankAccount = context.BankAccounts
               .Include(p => p.TransfersReceived)   
               .FirstOrDefault(p => p.PublicId == publicId);

            if (bankAccount == null)
                throw new NotFoundException("Nie znaleziono konta bankowego");

            return bankAccount.TransfersReceived.OrderByDescending(p => p.TransferDate).ToList();
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

        public List<Data.Entities.BankAccount> GetAllAccounts()
        {
            return context.BankAccounts.ToList();
        }

        public bool HasActiveBlockRequests(Guid accountId)
        {
            var account = context.BankAccounts
                .Include(p => p.BlockadeRequests)
                .FirstOrDefault(i => i.PublicId == accountId);  

            return account.BlockadeRequests.Any(p => p.IsActive);
        }

        public bool IsBlocked(Guid accountId)
        {
            var account = context.BankAccounts
               .Include(p => p.BlockadeRequests)
               .FirstOrDefault(i => i.PublicId == accountId);

            return account.BlockadeRequests.Any(p => p.IsAccepted.HasValue && p.IsAccepted.Value);
        }
    }
}

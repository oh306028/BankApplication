using AutoMapper;
using BankApplication.App.Exceptions;
using BankApplication.App.Modules.BankAccount.Models.Update;
using BankApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text;
using System;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.Data.Entities;
using BankApplication.Data.Enums;

namespace BankApplication.App.Services.BankAccount
{
    public interface IUpdateService
    {
        void Create(int userId, CreateBankAccountModel model);
        void SendTransfer(Guid accountId, SendTransferModel model);
        void SendBlockRequest(Guid accountId);
        void ManageBlockRequest(Guid accountId, BlockRequestModel model, int adminId);   
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

            if (!string.IsNullOrEmpty(model.Currency))
            {
                bankAccount.Currency = model.Currency;
            }
            else
            {
                bankAccount.Currency = "PLN";
            }


            if (model.InterestRate.HasValue)
                bankAccount.InteresetRate = model.InterestRate;
     
            bankAccount.ClientId = account.ClientId;

            if (model.Credit.HasValue)
                bankAccount.Balance = model.Credit.Value;
            else
                bankAccount.Balance = 0;

            bankAccount.BankAccountNumber = GenerateAccountNumber();

            context.BankAccounts.Add(bankAccount);
            context.SaveChanges();

        }

        public void SendTransfer(Guid accountId, SendTransferModel model)
        {
           var bankAccountTo = context.BankAccounts 
                .Include(p => p.TransfersReceived)
                .FirstOrDefault(p => p.BankAccountNumber == model.AccountToNumber);
                
            if (bankAccountTo == null)
                throw new NotFoundException("Nie znaleziono konta bankowego o takim numerze");

            var bankAccountFrom = context.BankAccounts
                .Include(p => p.TransfersReceived)
                .Single(p => p.PublicId == accountId);

            if (bankAccountFrom.Currency != bankAccountTo.Currency)
                throw new NotFoundException("Konto docelowe posiada inną walutę niż Twoje.");

            var transfer = new Transfer();
            transfer = mapper.Map<Transfer>(model);

            transfer.TransferDate = DateTime.Now;
            transfer.Status = (int)TransferStatus.Executed;

            transfer.AccountFromId = bankAccountFrom.Id;
            transfer.AccountToId = bankAccountTo.Id;

            using var transaction = context.Database.BeginTransaction();
            try
            {
                if (bankAccountFrom.Balance < model.Amount)
                    throw new InvalidOperationException("Niewystarczające środki na koncie.");

                context.Database.ExecuteSqlRaw(
                    "UPDATE BankAccounts SET Balance = Balance - {0} WHERE Id = {1}",
                    model.Amount,
                    bankAccountFrom.Id
                );

                context.Database.ExecuteSqlRaw(
                    "UPDATE BankAccounts SET Balance = Balance + {0} WHERE Id = {1}",
                    model.Amount,
                    bankAccountTo.Id
                );

                transfer.Status = (int)TransferStatus.Executed;
                context.Transfers.Add(transfer);

                context.SaveChanges(); 
                transaction.Commit();
            }
            catch
            {
                transfer.Status = (int)TransferStatus.Rejected;
                context.Transfers.Add(transfer); 
                context.SaveChanges();      
                transaction.Rollback();               
            }

        }

        public void SendBlockRequest(Guid accountId)
        {
            var account = context.BankAccounts
                .AsNoTracking()
                .Single(p => p.PublicId == accountId);

            var request = new BankAccountBlockRequest()
            {
                BankAccountId = account.Id,
                RequestDate = DateTime.Now,
                IsAccepted = false,
                IsActive = true
            };

            context.BankAccountBlockRequests.Add(request);
            context.SaveChanges();

        }

        public void ManageBlockRequest(Guid accountId, BlockRequestModel model, int adminId) 
        {
            var account = context.BankAccounts
                .Include(p => p.BlockadeRequests)
                .AsNoTracking()
                .Single(p => p.PublicId == accountId);

            var blockRequest = context.BankAccountBlockRequests
                .Single(p => p.PublicId == model.PublicId);

            blockRequest.AdminId = adminId;
            blockRequest.IsAccepted = model.Accepted;
            blockRequest.ManagedDate = DateTime.Now;
            blockRequest.IsActive = false;

            context.SaveChanges();

        }


        public string GenerateAccountNumber()
        {
            var sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                sb.Append(random.Next(0, 10));
            }

            for (int i = 0; i < 16; i++)
            {
                sb.Append(random.Next(0, 10));
            }

            string partialAccount = sb.ToString();
            string controlNumber = GenerateChecksum(partialAccount);

            return controlNumber + partialAccount;
        }

        private string GenerateChecksum(string accountWithoutChecksum)
        {

            string accountWithCountry = accountWithoutChecksum + "252100"; 

            var number = BigInteger.Parse(accountWithCountry);
            int checksum = 98 - (int)(number % 97);

            return checksum.ToString("D2");
        }

    }
}

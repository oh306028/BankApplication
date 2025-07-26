using BankApplication.Data.Enums;

namespace BankApplication.App.Modules.BankAccount.Models.Update
{
    public class CreateBankAccountModel
    {
        public BankAccountType Type { get; set; }
        public string Currency { get; set; }
        public decimal? InterestRate { get; set; } 
        public decimal? Credit { get; set; }    
    }
}   

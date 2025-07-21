using BankApplication.Data.Enums;

namespace BankApplication.App.Modules.BankAccount.Models.Update
{
    public class CreateBankAccountModel
    {
        public BankAccountType Type { get; set; }

        public string Currency { get; set; }
        public decimal? InteresetRate { get; set; }
    }
}

using BankApplication.App.Modules.Account.Models.Create;
using FluentValidation;

namespace BankApplication.App.Modules.BankAccount.Transfers.Models
{
    public class SendTransferModel
    {
        public string AccountToNumber { get; set; }               
        public decimal? Amount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class TransferModelValidator : AbstractValidator<SendTransferModel>  
    {
        public TransferModelValidator() 
        {
            RuleFor(p => p.AccountToNumber).NotEmpty().WithMessage("Numer konta jest wymagany");
            RuleFor(p => p.Title).NotEmpty().WithMessage("Tytuł jest wymagany");
            RuleFor(p => p.Amount).NotEmpty().WithMessage("Kwota jest wymagana");  
            
            RuleFor(p => p.Amount).GreaterThanOrEqualTo(0).WithMessage("Kwota nie może być mniejsza lub równa 0");
        }
    }
}

using FluentValidation;

namespace BankApplication.App.Modules.Account.Models
{
    public class AccountForm
    {
        public int? Id { get; set; }

        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class AccountFormValidator : AbstractValidator<AccountForm> {
        public AccountFormValidator()
        {
            RuleFor(e => e.Email).NotEmpty().MaximumLength(50).WithMessage("Email jest wymagany");
            RuleFor(e => e.Login).NotEmpty().MaximumLength(50).WithMessage("Login jest wymagany");
            RuleFor(e => e.Password).NotEmpty().MaximumLength(50).WithMessage("Hasło jest wymagane");
            RuleFor(e => e.ConfirmPassword).NotEmpty().Equal(p => p.Password).WithMessage("Hasła nie są takie same.");
        }
    }
}

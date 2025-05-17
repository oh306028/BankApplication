using FluentValidation;

namespace BankApplication.App.Modules.Account.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
    }

    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(p => p.Login).NotEmpty().WithMessage("Login jest wymagany");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email jest wymagany");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Hasło jest wymagane");
        }
    }
}

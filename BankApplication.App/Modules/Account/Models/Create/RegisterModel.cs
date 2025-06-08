using BankApplication.Data;
using FluentValidation;

namespace BankApplication.App.Modules.Account.Models.Create
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }    
        public string ConfirmPassword { get; set; }     
        
        public string ClientCode { get; set; }

    }

    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        private readonly AppDbContext _context;

        public RegisterModelValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(p => p.Login).NotEmpty().WithMessage("Login jest wymagany");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email jest wymagany");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Hasło jest wymagane").Equal(p => p.ConfirmPassword).WithMessage("Hasła muszą się zgadzać"); 
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("Hasło jest wymagane").Equal(p => p.Password).WithMessage("Hasła muszą się zgadzać");
            RuleFor(p => p.ClientCode).NotEmpty().WithMessage("Należy wprowadzić unikalny kod klienta");
            
        }   
    }
}

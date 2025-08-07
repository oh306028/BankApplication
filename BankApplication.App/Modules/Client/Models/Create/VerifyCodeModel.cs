namespace BankApplication.App.Modules.Client.Models.Create
{
    public class VerifyCodeModel
    {
        public int LoginAttemptId { get; set; }
        public string VerificationCode { get; set; }    
    }
}

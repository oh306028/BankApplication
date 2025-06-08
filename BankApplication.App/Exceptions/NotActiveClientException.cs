namespace BankApplication.App.Exceptions
{
    public class NotActiveClientException : Exception
    {
        public NotActiveClientException(string message) : base(message)
        {
            
        }
    }
}

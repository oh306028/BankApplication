using System.Security.Claims;

namespace BankApplication.App.Helpers.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int Id(this ClaimsPrincipal user) 
        {
            return Int32.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}

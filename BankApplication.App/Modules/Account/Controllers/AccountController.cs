using BankApplication.App.Modules.Account.Models;
using BankApplication.App.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.Account.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody]LoginModel model) 
        {
            var token =  accountService.Login(model);
                
            return Ok(token);
        }
    }
}

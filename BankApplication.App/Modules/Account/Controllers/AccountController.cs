using AutoMapper;
using BankApplication.App.Modules.Account.Models.Create;
using BankApplication.App.Modules.Client.Models.Create;
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
        private readonly IMapper mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginModel model)
        {
            var token = accountService.Login(model);

            return Ok(token);
        }


    }
      
 }

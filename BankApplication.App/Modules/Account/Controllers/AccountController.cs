using AutoMapper;
using BankApplication.App.Helpers.Extensions;
using BankApplication.App.Modules.Account.Models.Create;
using BankApplication.App.Modules.Account.Models.Details;
using BankApplication.App.Modules.Client.Models.Create;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.App.Services.Account;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<SuccededLoginModel>> Login([FromBody] LoginModel model)
        {
            var succededModel = await accountService.Login(model);

            return Ok(succededModel);   
        }

        [HttpPost("verify-code")]
        public async Task<ActionResult<string>> VerifyCode([FromBody] VerifyCodeModel model)    
        {
            var token = await accountService.VerifyCode(model); 

            return Ok(token);   
        }

        [HttpPost("register")]
        public ActionResult<string> Register([FromBody] RegisterModel model)    
        {
            accountService.Register(model); 

            return Ok();
        }

        [HttpGet("isAdmin")]
        [Authorize]
        public ActionResult<bool> IsAdmin()  
        {
            var response = accountService.IsAdmin(User.Id());
            return Ok(response);
        }

        [HttpGet("profile")]
        [Authorize]
        public ActionResult<ProfileDetails> GetDetails()
        {
            var response = accountService.GetDetails(User.Id());   
            return Ok(response);

        }


    }

}

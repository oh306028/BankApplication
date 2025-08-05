using AutoMapper;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.App.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.Account.Controllers
{
    [Route("api/accounts/dictionary")]
    [ApiController]
    [Authorize]
    public class DictionaryController : ControllerBase
    {

        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public DictionaryController(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [HttpGet("admins")]
        public ActionResult<List<ClientDetails>> GetAdmins()
        {
            var result = accountService.GetAdmins();
            return Ok(result);
        }
    }
}

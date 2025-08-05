using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.App.Services.BankAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.Client.Controllers
{

    [Route("api/clients/dictionary")]
    [ApiController]
    [Authorize]
    public class DictionaryController : ControllerBase
    {
        private readonly App.Services.Client.IDetailService service;

        public DictionaryController(App.Services.Client.IDetailService service)
        {
            this.service = service; 
        }


        [HttpGet("list")]
        [Authorize]
        public ActionResult<List<ClientDetails>> GetList()
        {
            var clients = service.List();
            return Ok(clients);
        }

        [HttpGet("login-attempts")]
        public ActionResult<List<LogginDetails>> LoginAttempts()    
        {
            var clients = service.LoginAttempts();  
            return Ok(clients);
        }

        [HttpGet("block-requests")]
        public ActionResult<List<BlockRequestDetails>> BlockRequests()        
        {   
            var blockRequests = service.BlockRequests();  
            return Ok(blockRequests);
        }

    }
}

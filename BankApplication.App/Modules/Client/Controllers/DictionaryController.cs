using BankApplication.App.Helpers.Extensions;
using BankApplication.App.Modules.BankAccount.Models.Update;
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
        private readonly App.Services.Client.IUpdateService clients;

        public DictionaryController(App.Services.Client.IDetailService service, App.Services.Client.IUpdateService clients)
        {
            this.service = service;
            this.clients = clients;
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

        [HttpGet("client-requests")]
        public ActionResult<List<ClientRequestDetails>> ClientRequests()    
        {
            var blockRequests = service.ClientRequests();   
            return Ok(blockRequests);
        }

        [HttpPost("{accountId}/manage-client-request")]
        public ActionResult ManageRequest(Guid accountId, BlockRequestModel model)
        {
            clients.ManageClientRequest(accountId, model, User.Id());
            return Ok();
        }

    }
}

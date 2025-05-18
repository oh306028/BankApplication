using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.App.Services.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.Client.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly IDetailService service;

        public DetailsController(IDetailService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDetails> Fetch([FromRoute] Guid id)
        {
           var client = service.Fetch(id);
            return Ok(client);
        }

        [HttpGet]
        public ActionResult<List<ClientDetails>> GetList()
        {
            var clients = service.List();
            return Ok(clients);
        }

    }
}

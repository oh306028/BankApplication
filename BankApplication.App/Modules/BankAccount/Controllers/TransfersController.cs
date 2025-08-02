using AutoMapper;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.App.Services.BankAccount;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.BankAccount.Controllers
{
    [Route("api/bank-accounts/{accountId}/transfers")]
    [ApiController]
    public class TransfersController : ControllerBase   
    {   
        private readonly IUpdateService transfers; 
        private readonly IDetailService transfersDetails;
        private readonly IMapper mapper;
            
        public TransfersController(IUpdateService update, IDetailService details, IMapper mapper)    
        {
            transfers = update;
            transfersDetails = details;
            this.mapper = mapper;
        }


        [HttpGet("sent")]
        public ActionResult<List<TransferDetails>> GetSentTransfers([FromRoute] Guid accountId)     
        {
            var transfers = transfersDetails.GetTransfersSent(accountId);
            return Ok(mapper.Map<List<TransferDetails>>(transfers));
           
        }

        [HttpGet("received")]
        public ActionResult<List<TransferDetails>> GetReceivedTransfers([FromRoute] Guid accountId)
        {
            var transfers = transfersDetails.GetTransfersReceived(accountId);
            return Ok(mapper.Map<List<TransferDetails>>(transfers));
        }

        [HttpGet]
        public ActionResult<List<TransferDetails>> GetList([FromRoute] Guid accountId)  
        {
            var transfers = transfersDetails.GetList(accountId);    
            return Ok(mapper.Map<List<TransferDetails>>(transfers));
        }

        [HttpPost]
        public ActionResult Send([FromRoute] Guid accountId, SendTransferModel model)
        {
            transfers.SendTransfer(accountId, model);
            return Ok();
        }

    }
}

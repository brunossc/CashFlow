using CashFlow.Financial.API.Application.CashOperation.Commands.Requests;
using CashFlow.Financial.API.Controllers.Base;
using CashFlow.Financial.API.Domain.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Financial.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CashOperationController : ControllerAppBase
    {
        private readonly ILogger<CashOperationController> _logger;
        private readonly IMediator _mediator;
        private readonly ICashOperationService _service;

        public CashOperationController(ILogger<CashOperationController> logger, IMediator mediator, ICashOperationService service)
        {
            _logger = logger;
            _mediator = mediator;
            _service = service;
        }

        [HttpPost("AddCredit")]
        public async Task<IActionResult> Credit(CreditCashOperationCommandsRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("AddDebit")]
        public async Task<IActionResult> Debit(DebitCashOperationCommandsRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetAllOperations")]
        public async Task<IActionResult> Get()
        {
            var response = await _service.GetAll();
            return Ok(response);
        }
    }
}

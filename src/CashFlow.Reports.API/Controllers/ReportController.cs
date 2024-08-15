using CashFlow.Financial.API.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Reports.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ICashOperationService _service;

        public ReportController(ICashOperationService service)
        {
            _service = service;
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport()
        {
            var result = await Task.FromResult(_service.GetConsolidateByDay());
            return Ok(result);
        }
    }
}

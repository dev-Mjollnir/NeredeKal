using HotelService.Application.Report.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("hotel")]
        public async Task<IActionResult> GetHotelReport([FromBody] GetHotelReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

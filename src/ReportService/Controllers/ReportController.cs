using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportService.Application.Query;

namespace ReportService.Controllers
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetReportsQuery());
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetReportByIdQuery { Id = id});
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }
    }
}

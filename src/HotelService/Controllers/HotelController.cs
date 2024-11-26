using HotelService.Application.Contact.Command;
using HotelService.Application.Hotel.Command;
using HotelService.Application.Hotel.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] CreateHotelCommand command)
        {  
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var result = await _mediator.Send(new GetHotelsQuery());
            if (!result.Any())
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelDetails([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetHotelByIdQuery { Id = id});
            if(result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            var result = await _mediator.Send(new DeleteHotelCommand { Id = id });
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpPost("{hotelId}/contact")]
        public async Task<IActionResult> AddHotelContact([FromRoute] Guid hotelId, [FromBody] AddHotelContactCommand command)
        {
            command.HotelId = hotelId;
            var result = await _mediator.Send(command);
            if (result == Guid.Empty)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{hotelId}/contact/{contactId}")]
        public async Task<IActionResult> DeleteHotelContact(Guid hotelId, Guid contactId)
        {
            var result = await _mediator.Send(new DeleteHotelContactCommand { HotelId = hotelId, ContactId = contactId });
            if (!result)
                return BadRequest();
            return Ok();
        }     
    }
}

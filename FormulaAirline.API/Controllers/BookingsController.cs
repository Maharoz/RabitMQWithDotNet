
using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMessageProducer _messageProducer;

        public static readonly List<Booking> _bookings = new();
        public BookingsController(ILogger<WeatherForecastController> logger,
            IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _bookings.Add(newBooking);
            _messageProducer.SendingMessages<Booking>(newBooking);
            return Ok();
        }
    }
}
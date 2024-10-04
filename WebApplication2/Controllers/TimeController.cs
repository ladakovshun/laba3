using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class TimeController : Controller
    {

        private readonly TimeService _timeService;

        public TimeController(TimeService timeService)
        {
            _timeService = timeService;
        }

        [HttpGet("timeofday")]
        public IActionResult GetTimeOfDay()
        {
            return Ok(_timeService.GetTimeOfDay());
        }
    }
}
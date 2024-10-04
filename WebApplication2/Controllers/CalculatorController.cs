using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly CalcService _calcService;

        public CalculatorController(CalcService calcService)
        {
            _calcService = calcService;
        }

        [HttpGet("add")]
        public IActionResult Add(int a, int b)
        {
            return Ok(_calcService.Add(a, b));
        }

        [HttpGet("subtract")]
        public IActionResult Subtract(int a, int b)
        {
            return Ok(_calcService.Subtract(a, b));
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int a, int b)
        {
            return Ok(_calcService.Multiply(a, b));
        }

        [HttpGet("divide")]
        public IActionResult Divide(int a, int b)
        {
            try
            {
                return Ok(_calcService.Divide(a, b));
            }
            catch (DivideByZeroException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

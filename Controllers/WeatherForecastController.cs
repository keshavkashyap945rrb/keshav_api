using Microsoft.AspNetCore.Mvc;
using Project_WebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project_WebAPI.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeatherForecastController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            var forecasts = _context.WeatherForecasts.ToList();
            return Ok(forecasts); // Wrap the list in Ok() to return an ActionResult
        }
        
        [HttpGet("{id}")]
        public ActionResult<WeatherForecast> GetById(int id)
        {
            var forecast = _context.WeatherForecasts.Find(id);
            if (forecast == null)
            {
                return NotFound();
            }

            return Ok(forecast);
        }

        [HttpGet("{id},{TemperatureC}")]
        public ActionResult<WeatherForecast> GetById(int id,int TemperatureC)
        {
            var forecast = _context.WeatherForecasts.Where(x=>x.Id==id && x.TemperatureC==TemperatureC );
            if (forecast == null)
            {
                return NotFound();
            }

            return Ok(forecast);
        }

        [HttpPost("create")]
        public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest("WeatherForecast data is null.");
            }

            try
            {
                WeatherForecast obj = new WeatherForecast
                {
                    Date = forecast.Date,
                    TemperatureC = forecast.TemperatureC,
                    Summary = forecast.Summary,
                };
                _context.WeatherForecasts.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetById), new { id = forecast.Id }, forecast);
        }
        [HttpPost("update")]
        public ActionResult<WeatherForecast> Update([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest("WeatherForecast data is null.");
            }

            var dt = _context.WeatherForecasts.FirstOrDefault(x => x.Id == forecast.Id);

            if (dt == null)
            {
                return NotFound("WeatherForecast not found.");
            }

            try
            {
                dt.TemperatureC = forecast.TemperatureC;
                dt.Date = forecast.Date;
                dt.Summary = forecast.Summary;

                _context.WeatherForecasts.Update(dt);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetById), new { id = forecast.Id }, forecast);
        }

        //[HttpGet("emp")]
        //public ActionResult<EmployeeMaster> GetEmp()
        //{
        //    var forecast = _context.Employee.ToList();
        //    if (forecast == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(forecast);
        //}
    }
}

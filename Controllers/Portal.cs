using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_WebAPI.Models;
using System.Data;

namespace Project_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Portal : Controller
    {
        private readonly AppDbContext _context;
        public Portal(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("login")]
        public ActionResult<EmployeeMaster> GetEmp()
        {
            var forecast = _context.login.ToList();
            if (forecast == null)
            {
                return NotFound();
            }

            return Ok(forecast);
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

        [HttpGet("LoginAPI")]
        public ActionResult<WeatherForecast> GetById(string username, string password)
        {
            var forecast = _context.login.Where(x => x.Email == username && x.Password == password);
            if (forecast.IsNullOrEmpty())
            {
                return NotFound("Authentication Failed");
            }
            //// if(forecast.IsNullOrEmpty<>)
            //// Convert the result to a DataTable
            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("Email", typeof(string));
            //dataTable.Columns.Add("Password", typeof(string));
            //// Add other columns as needed

            //foreach (var item in forecast)
            //{
            //    DataRow row = dataTable.NewRow();
            //    row["Email"] = item.Email;
            //    row["Password"] = item.Password;
            //    // Add other columns as needed

            //    dataTable.Rows.Add(row);
            //}
            return Ok(forecast);
        }

        [HttpGet("LoginMobileAPI")]
        public ActionResult<WeatherForecast> LoginMobileAPI(string Mobile)
        {
            var forecast = _context.login.Where(x => x.Test == Mobile);
            if (forecast == null)
            {
                return NotFound("Authentication Failed");
            }

            //// if(forecast.IsNullOrEmpty<>)
            //// Convert the result to a DataTable
            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("Email", typeof(string));
            //dataTable.Columns.Add("Password", typeof(string));
            //// Add other columns as needed

            //foreach (var item in forecast)
            //{
            //    DataRow row = dataTable.NewRow();
            //    row["Email"] = item.Email;
            //    row["Password"] = item.Password;
            //    // Add other columns as needed

            //    dataTable.Rows.Add(row);
            //}
            return Ok(forecast);
        }

        [HttpGet("LoginMobilePassAPI")]
        public ActionResult<WeatherForecast> LoginMobilePassAPI(string Mobile, string Password)
        {
            var forecast = _context.login.Where(x => x.Mobile == Mobile && x.Password == Password);
            if (forecast.IsNullOrEmpty())
            {
                return NotFound("Authentication Failed");
            }
            //// if(forecast.IsNullOrEmpty<>)
            //// Convert the result to a DataTable
            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("Email", typeof(string));
            //dataTable.Columns.Add("Password", typeof(string));
            //// Add other columns as needed

            //foreach (var item in forecast)
            //{
            //    DataRow row = dataTable.NewRow();
            //    row["Email"] = item.Email;
            //    row["Password"] = item.Password;
            //    // Add other columns as needed

            //    dataTable.Rows.Add(row);
            //}
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
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_WebAPI.Models;

namespace Project_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("emp")]
        public ActionResult<EmployeeMaster> GetEmp()
        {
            var forecast = _context.Employee.ToList();
            if (forecast == null)
            {
                return NotFound();
            }

            return Ok(forecast);
        }
    }
}

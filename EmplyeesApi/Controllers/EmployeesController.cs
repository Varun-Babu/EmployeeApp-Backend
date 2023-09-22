using EmplyeesApi.Data;
using EmplyeesApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmplyeesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public EmployeesController(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.VbEmployees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _context.VbEmployees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmoloyeeById([FromRoute] Guid id)
        {
            var employee = await _context.VbEmployees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditEmployee([FromRoute]Guid id,[FromBody]Employee updatedEmployee)
        {
            var exist = await _context.VbEmployees.FindAsync(id);
            if (exist == null)
            {
                return NotFound();
            }
            if(updatedEmployee.Name != exist.Name) {
            exist.Name = updatedEmployee.Name;
            }
            if (updatedEmployee.Email != exist.Email)
            {
                exist.Email = updatedEmployee.Email;
            }
            if (updatedEmployee.Salary != exist.Salary)
            {
                exist.Salary = updatedEmployee.Salary;
            }
            if (updatedEmployee.Phone != exist.Phone)
            {
                exist.Phone = updatedEmployee.Phone;
            }
            if (updatedEmployee.Department != exist.Department)
            {
                exist.Department = updatedEmployee.Department;
            }
            await _context.SaveChangesAsync();
            return Ok(exist);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var exist = await _context.VbEmployees.FindAsync(id);
            if(exist == null)
            {
                return NotFound();
            }
            _context.VbEmployees.Remove(exist);
            await _context.SaveChangesAsync();
            return Ok(exist);
        }
    }

}

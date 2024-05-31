using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employess= dbContext.Employees.ToList();
            return Ok(employess);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if(employee == null) { return NotFound(); }
            return Ok(employee);
        }
        [HttpPost]
        public IActionResult AddEmployee(Guid id,AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Email = addEmployeeDto.Email,
                Name = addEmployeeDto.Name,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };
            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updatedEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);
            employee.Email = updatedEmployeeDto.Email;
            employee.Name = updatedEmployeeDto.Name;
            employee.Phone = updatedEmployeeDto.Phone;
            employee.Salary = updatedEmployeeDto.Salary;
            dbContext.SaveChanges();
            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            dbContext.Employees.Remove(employee);    
            dbContext.SaveChanges();
            return Ok(employee);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Common.Repositories;
using OpticsWebApi.Entities;
using OpticsWebApi.RequestModels;

namespace OpticsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        private readonly OpticsDBContext conText;

        public EmployeesController(OpticsDBContext context)
        {
            conText = context;
        }


        // GET: api/Employee
        [HttpGet]
       
        public IActionResult GetEmployee([FromQuery]string? name, int pageNumber=1, int pageSize=10)
        {
            List<Employee> employees = new List<Employee>();
            if (!string.IsNullOrEmpty(name))
            {
                employees = conText.Employees.Where(c => c.EmployeeName == name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                employees = conText.Employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return Ok(employees);
            
        }

       

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = conText.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, CreateEmployeeModel updatedEmployee)
        {

            Employee employee = conText.Employees
                                .Where(i => i.Id == id)
                                .FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeName = updatedEmployee.EmployeeName;
            employee.EmployeePosition = updatedEmployee.EmployeePosition;
            employee.EmployeePhone = updatedEmployee.EmployeePhone;
            employee.Email = updatedEmployee.Email;
            employee.Salary=updatedEmployee.Salary;
            

            conText.Employees.Update(employee);
            conText.SaveChanges();

            conText.Dispose();

            return Ok(employee);
        }

        //POST: api/Employees/CreateEmployees
        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeModel employeeRequest)
        {
            Employee employee = new Employee()
            {
                EmployeeName = employeeRequest.EmployeeName,
                EmployeePosition = employeeRequest.EmployeePosition,
                EmployeePhone = employeeRequest.EmployeePhone,
                Email = employeeRequest.Email,
                Salary = employeeRequest.Salary,    
                CreatedAt = DateTime.Now,

            };
            conText.Employees.Add(employee);
            conText.SaveChanges();

            return Created();
        }


        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = conText.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            conText.Employees.Remove(employee);
            conText.SaveChanges();

            return NoContent();
        }


    }
}

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
using Newtonsoft.Json;

namespace OpticsWebApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController
    {
        private readonly OpticsDBContext conText;

        public CustomersController(OpticsDBContext context)
        {
            conText = context;
        }


        // GET: api/Customers
        [HttpGet]
        public IActionResult GetCustomer([FromQuery]string? name,int pageNumber=1, int pageSize=10)
        {
            List<Customer> customers = new List<Customer>();
            if (!string.IsNullOrEmpty(name))
            {
                customers=conText.Customers.Where(c => c.CustomerName==name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                customers=conText.Customers.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            }
            return Ok(customers);
        }


        // GET: api/Customers/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = conText.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, CreateCustomerModel updatedCustomer)
        {

            Customer customer = conText.Customers
                                .Where(i => i.Id == id)
                                .FirstOrDefault();
            if (customer == null)
            {
                return NotFound();
            }

            customer.CustomerName = updatedCustomer.CustomerName;
            customer.CustomerEmail = updatedCustomer.CustomerEmail;
            customer.CustomerPhone = updatedCustomer.CustomerPhone;
            customer.CustomerAddress = updatedCustomer.CustomerAddress;
            customer.IsFemale= updatedCustomer.IsFemale;
            
           
            conText.Customers.Update(customer);
            conText.SaveChanges();

            conText.Dispose();

            return Ok(customer);
        }

        //POST: api/Customers/CreateCustomer
        [HttpPost]
        public IActionResult CreateCustomer(CreateCustomerModel customerRequest)
        {
            Customer customer = new Customer()
            {
                
                CustomerName = customerRequest.CustomerName,
                CustomerEmail = customerRequest.CustomerEmail,
                CustomerPhone = customerRequest.CustomerPhone,
                CustomerAddress = customerRequest.CustomerAddress,
                IsFemale = customerRequest.IsFemale,
                CreatedAt = DateTime.Now,
                
            };
            conText.Customers.Add(customer);
            conText.SaveChanges();

            return Created();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = conText.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            conText.Customers.Remove(customer);
            conText.SaveChanges();

            return NoContent();
        }


    }


}

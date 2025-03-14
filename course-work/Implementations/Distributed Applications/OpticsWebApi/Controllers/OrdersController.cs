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
using OpticsWebApi.ResultModels;

namespace OpticsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly OpticsDBContext conText;

        public OrdersController(OpticsDBContext context)
        {
            conText = context;
        }

        // GET: api/Orders shipping adress
        [HttpGet]
       
        public IActionResult GetOrders([FromQuery] string? shippingAdress, int pageNumber = 1, int pageSize = 10)
        {
            List<Order> orders = new List<Order>();
            if (!string.IsNullOrEmpty(shippingAdress))
            {
                orders = conText.Orders.Where(c => c.ShippingAddress == shippingAdress).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                orders = conText.Orders.Skip((pageNumber - 1) * pageSize).Take(pageSize).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = conText
                .Orders
                .Select(o => new GetOrderModel
                {
                    CustomerID = o.CustomerID,
                    OrderID = o.Id,
                    EmployeeID = o.EmployeeID,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.OrderDetails.Sum(od => od.Quantity * od.Product.Price),
                })
                .FirstOrDefault(o=>o.OrderID==id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
       
        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, CreateOrderModel updatedOrder)
        {

            Order order= conText.Orders
                                .Where(i => i.Id == id)
                                .FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }

            order.OrderDate = updatedOrder.OrderDate;
            order.CustomerID = updatedOrder.CustomerID;
            order.EmployeeID = updatedOrder.EmployeeID;
            order.ShippingAddress = updatedOrder.ShippingAddress;

            conText.Orders.Update(order);
            conText.SaveChanges();

            conText.Dispose();

            return Ok(order);
        }

        // POST: api/Orders
        
        [HttpPost] 
            public IActionResult CreateOrder(CreateOrderModel orderRequest)
            {
            Customer customer=conText.Customers.FirstOrDefault(c => c.Id==orderRequest.CustomerID);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

             Employee employee = conText.Employees.FirstOrDefault(e => e.Id == orderRequest.EmployeeID);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            Order order = new Order()
            {
                OrderDate = orderRequest.OrderDate,
                CustomerID = orderRequest.CustomerID,
                EmployeeID = orderRequest.EmployeeID,
                ShippingAddress = orderRequest.ShippingAddress,
                CreatedAt = DateTime.Now,
            };

            conText.Orders.Add(order);
            conText.SaveChanges();

            return Created();
            }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = conText.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            conText.Orders.Remove(order);
            conText.SaveChanges();

            return NoContent();
        }

        
    }
}

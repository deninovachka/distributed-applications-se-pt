using Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpticsWebApi.Entities;
using OpticsWebApi.RequestModels;
using OpticsWebApi.ResultModels;
using System.Drawing.Printing;

namespace OpticsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : BaseController
    {
        private readonly OpticsDBContext conText;

        public OrderDetailsController(OpticsDBContext context)
        {
            conText = context;
        }

        // GET: api/OrderDetails is shipped
        [HttpGet]
        public IActionResult GetOrderDetails(bool isShipped, int pageNumber=1, int pageSize=10)
        {
            List<GetOrderDetailsModel> orderDetailsModels = new List<GetOrderDetailsModel>();
            if (isShipped)
            {
                orderDetailsModels = conText
           .OrderDetails
           .Select(o => new GetOrderDetailsModel
           {
               OrderID = o.OrderID,
               ProductID = o.ProductID,
               Quantity = o.Quantity,
               UnitPrice = o.Product.Price,
               TotalPrice = o.Quantity * o.Product.Price,
               IsShipped = o.IsShipped,
           })
                .Where(o => o.IsShipped)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else 
            {
                orderDetailsModels = conText
         .OrderDetails
         .Select(o => new GetOrderDetailsModel
         {
             OrderID = o.OrderID,
             ProductID = o.ProductID,
             Quantity = o.Quantity,
             UnitPrice = o.Product.Price,
             TotalPrice = o.Quantity * o.Product.Price,
             IsShipped = o.IsShipped,
         })
         .Where(o => !o.IsShipped)
         .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            }
            return Ok(orderDetailsModels);
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public IActionResult GetOrderDetails(int id)
        {
            var orderDetailsQuery = conText
            .OrderDetails
            .Include(o => o.Product)
            .FirstOrDefault(o => o.OrderID == id);

            if (orderDetailsQuery == null)
            {
                return NotFound();
            }


            var orderDetails = new GetOrderDetailsModel
            {
                OrderID = orderDetailsQuery.OrderID,
                ProductID = orderDetailsQuery.ProductID,
                Quantity = orderDetailsQuery.Quantity,
                UnitPrice = orderDetailsQuery.Product.Price,
                TotalPrice = orderDetailsQuery.Quantity * orderDetailsQuery.Product.Price,

            };

            return Ok(orderDetails);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public IActionResult PutOrderDetails(int id, CreateOrderDetailsModel updatedOrderDetails)
        {

            OrderDetails orderDetails = conText.OrderDetails
                                .Where(i => i.Id == id)
                                .FirstOrDefault();
            if (orderDetails == null)
            {
                return NotFound();
            }

            orderDetails.OrderID = updatedOrderDetails.OrderID;
            orderDetails.ProductID = updatedOrderDetails.ProductID;
            orderDetails.Quantity = updatedOrderDetails.Quantity;
            orderDetails.IsShipped = updatedOrderDetails.IsShipped;
            //createat


            conText.OrderDetails.Update(orderDetails);
            conText.SaveChanges();

            conText.Dispose();

            return Ok(orderDetails);
        }

        // POST: api/OrderDetails
        [HttpPost]
        public IActionResult CreateOrderDetails(CreateOrderDetailsModel orderDetailsRequest)
        {
            Order order = conText.Orders.FirstOrDefault(o => o.Id == orderDetailsRequest.OrderID);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            Product product = conText.Products.FirstOrDefault(p => p.Id == orderDetailsRequest.ProductID);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            OrderDetails orderDetails = new OrderDetails()
            {
                OrderID = orderDetailsRequest.OrderID,
                ProductID = orderDetailsRequest.ProductID,
                Quantity = orderDetailsRequest.Quantity,
                IsShipped = orderDetailsRequest.IsShipped,
                CreatedAt = DateTime.Now,
            };

            conText.OrderDetails.Add(orderDetails);
            conText.SaveChanges();

            return Created();
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetails(int id)
        {
            var orderDetails = conText.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            conText.OrderDetails.Remove(orderDetails);
            conText.SaveChanges();

            return NoContent();
        }


    }
}


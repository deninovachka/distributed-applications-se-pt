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
    public class ProductsController : BaseController
    {
        private readonly OpticsDBContext conText;

        public ProductsController(OpticsDBContext context)
        {
            conText = context;
        }


        // GET: api/Products
        [HttpGet]
     
        public IActionResult GetProducts([FromQuery] string? brand, int pageNumber = 1, int pageSize = 10)
        {
            List<Product> products = new List<Product>();
            if (!string.IsNullOrEmpty(brand))
            {
                products = conText.Products.Where(c => c.Brand == brand).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); ;
            }
            else
            {
                products = conText.Products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name ="GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product =  conText.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, CreateProductModel updatedProduct)
        {
            
            Product product = conText.Products
                                .Where(i => i.Id == id)
                                .FirstOrDefault();
            if (product==null)
            {
                return NotFound();
            }
            product.Brand = updatedProduct.Brand;
            product.Model = updatedProduct.Model;
            product.Color = updatedProduct.Color;
            product.Category = updatedProduct.Category;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;

            conText.Products.Update(product);
            conText.SaveChanges();

            conText.Dispose();

            return Ok(product);
        }

        //POST: api/Products/CreateProduct
        [HttpPost]
        public IActionResult CreateProduct(CreateProductModel productRequest)
        {
            Product product = new Product()
            {
                Brand = productRequest.Brand,
                Model = productRequest.Model,
                Color = productRequest.Color,
                Category = productRequest.Category,
                Price = productRequest.Price,
                Quantity = productRequest.Quantity,
                CreatedAt = DateTime.Now,
            };
            conText.Products.Add(product);
            conText.SaveChanges();

            return Created();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product =  conText.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            conText.Products.Remove(product);
            conText.SaveChanges();

            return NoContent();
        }

        
    }
}

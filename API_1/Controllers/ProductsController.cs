using API_1.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_1.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        public static List<Product> products = new List<Product>
        {
            new Product{Id=1, Name="Alma", Price=5,Stok=45,IsActive=true },
            new Product{Id=2, Name="Banan", Price=2,Stok=89,IsActive=true },
            new Product{Id=3, Name="Armud", Price=3,Stok=78,IsActive=true }
        };
        [HttpGet]
        [Authorize(Policy = "ReadProduct")]
        public IActionResult GetProducts()
        {
            return Ok(products);
        }

        [HttpPost("{id}")]
        [Authorize(Policy = "UpdateProduct")]
        public IActionResult UpdateProduct(int id)
        {
            var p = products.Find(x=>x.Id == id);
            return Ok(p.Name + " deyishdirildi");
        }

        [HttpPost]
        [Authorize(Policy = "CreateProduct")]
        public IActionResult CreateProduct(Product product)
        {
            products.Add(product);
            return Ok(true);
        }
    }
}

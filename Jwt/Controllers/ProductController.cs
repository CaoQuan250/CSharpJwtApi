using Jwt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.Controllers
{
    public class ProductController : RootController
    {
        private readonly JwtcsharpContext context;
        public ProductController(JwtcsharpContext _context)
        {
            context = _context;
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return Ok(product);
        }
        [HttpGet]
        public IActionResult Get(){
            return Ok(context.Products);
        }
        [HttpGet]
        public IActionResult GetDetial(long id)
        {
            return Ok(context.Products.Find(id));
        }
        [HttpPut]
        public IActionResult Edit(Product product) {
            context.Products.Update(product);
            context.SaveChanges();
            return Ok(product);
        }
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var product = context.Products.Find(id);
            context.Products.Remove(product);
            context.SaveChanges();
            return Ok(product);
        }
    }
}
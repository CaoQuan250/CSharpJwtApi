using Jwt.Models;
using Jwt.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.Controllers
{
    public class CustomerController : RootController
    {
        private readonly JwtcsharpContext context;

        public CustomerController(JwtcsharpContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(context.Customers);
        }
        [HttpGet]
        public IActionResult Details(long id) {
            Customer customer = context.Customers.Find(id);
            var detail = new DetailResponse
            {
                Id = id,

                Name = customer.Name,

                Age = customer.Age,

                Gender = customer.Gender,

                Address = customer.Address,

                Username = customer.Username,

                Products = getProductByCus(id),

            };
            return Ok(detail);
        }
        [HttpPut]
        public IActionResult Edit(Customer customer) {
            context.Customers.Update(customer);
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
            context.SaveChanges();
            return Ok("Success");
        }

        private List<Product> getProductByCus(long cusId)
        {
            return context.Orders.Where(o => o.Customer.Id == cusId).Select(o => o.Product).ToList();
        }


    }
}

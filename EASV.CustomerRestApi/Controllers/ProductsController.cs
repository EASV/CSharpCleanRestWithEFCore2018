using System;
using System.Collections.Generic;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EASV.CustomerRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        // GET api/products -- READ All
        [HttpGet]
        public ActionResult<FilteredList<Product>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_productService.GetAllFiltered(filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/products/5 -- READ By Id
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater then 0");
            
            return Ok(_productService.FindById(id));
        }

        // POST api/products -- CREATE
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            try
            {
                return Ok(_productService.Create(product));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        
        // PUT api/products/5 -- Update
        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product product)
        {
            if (id < 1 || id != product.Id)
            {
                return BadRequest("Parameter Id and product ID must be the same");
            }

            return Ok(_productService.Update(product));
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            return Ok($"Product with Id: {id} is Deleted");
        }
    }
}
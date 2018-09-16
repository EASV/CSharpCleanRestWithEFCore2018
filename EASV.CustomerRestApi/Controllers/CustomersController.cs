using System.Collections.Generic;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EASV.CustomerRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        // GET api/customers -- READ All
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            ///Customers with all there orders? NO
            return _customerService.GetAllCustomers();
        }

        // GET api/customers/5 -- READ By Id
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater then 0");
            
            //return _customerService.FindCustomerById(id);
            return _customerService.FindCustomerByIdIncludeOrders(id);
        }

        // POST api/customers -- CREATE
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                return BadRequest("Firstname is Required for Creating Customer");
            }

            if (string.IsNullOrEmpty(customer.LastName))
            {
                return BadRequest("LastName is Required for Creating Customer");
            }
            //return StatusCode(503, "Horrible Error CALL Tech Support");
            return _customerService.CreateCustomer(customer);
        }
        
        // PUT api/customers/5 -- Update
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody] Customer customer)
        {
            if (id < 1 || id != customer.Id)
            {
                return BadRequest("Parameter Id and customer ID must be the same");
            }

            return Ok(_customerService.UpdateCustomer(customer));
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            var customer = _customerService.DeleteCustomer(id);
            if (customer == null)
            {
                return StatusCode(404, "Did not find Customer with ID " + id);
            }

            return Ok($"Customer with Id: {id} is Deleted");
        }
    }
}

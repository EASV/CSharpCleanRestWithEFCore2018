using System;
using System.Collections.Generic;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EASV.CustomerRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypesController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerTypesController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        // GET api/customers -- READ All
        [HttpGet]
        public ActionResult<List<CustomerType>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_customerService.ReadCustomerTypes());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }
    }
}
using System;
using System.Collections.Generic;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EASV.CustomerRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        // GET api/orders -- READ All
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(_orderService.GetAllOrders());
        }

        // GET api/orders/5 -- READ By Id
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater then 0");
            
            return Ok();
        }

        // POST api/orders -- CREATE
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                return Ok(_orderService.CreateOrder(order));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        
        // PUT api/orders/5 -- Update
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (id < 1 || id != order.Id)
            {
                return BadRequest("Parameter Id and order ID must be the same");
            }

            return Ok();
        }

        // DELETE api/orders/5
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            return Ok($"Order with Id: {id} is Deleted");
        }
    }
}
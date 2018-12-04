using System;
using System.Collections.Generic;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.Entity;
using CustomerApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EASV.CustomerRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        // GET api/orders -- READ All
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_userService.GetAllUsers(filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
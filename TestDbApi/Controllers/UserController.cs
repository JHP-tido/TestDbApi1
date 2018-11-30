using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestDbApi.Interface;
using TestDbApi.Models;
using TestDbApi.Models.Extensions;

namespace TestDbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public UserController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        //Implement LoggerManager NLog
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers();
                return Ok(users);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/customernull")]
        public IActionResult GetUserById(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);
 
                if (user.IsEmptyObject())
                { 
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/customers")]
        public IActionResult GetUserWithDetails(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserWithDetails(id);

                if (user.IsEmptyObject())
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserWithOutCustomerInfo(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserWithOutCustomerInfo(id);

                if (user.IsEmptyObject())
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/notshowpass")]
        public IActionResult GetUserWithOutPass(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserWithOutPass(id);

                if (user.IsEmptyObject())
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]User user)
        {
            try
            {
                if(user.IsObjectNull())
                {
                    return BadRequest("User object is null");
                }
 
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.User.CreateUser(user);
 
                return Ok("Created");
                //return CreatedAtRoute("UserById", new { id = user.UserId}, user);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromRoute]Guid id, [FromBody]User user)
        {
            try
            {
                if (user.IsObjectNull())
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var dbUser = _repository.User.GetUserById(id);
                if (dbUser.IsEmptyObject())
                {
                    return NotFound();
                }

                _repository.User.UpdateUser(dbUser, user);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
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
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _repository.User.GetAllUsersAsync();
                return Ok(users);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/customernull")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _repository.User.GetUserByIdAsync(id);
 
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
        public async Task<IActionResult> GetUserWithDetails(Guid id)
        {
            try
            {
                var user = await _repository.User.GetUserWithDetailsAsync(id);

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
        public async Task<IActionResult> GetUserWithOutCustomerInfo(Guid id)
        {
            try
            {
                var user = await _repository.User.GetUserWithOutCustomerInfoAsync(id);

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
        public async Task<IActionResult> GetUserWithOutPass(Guid id)
        {
            try
            {
                var user = await _repository.User.GetUserWithOutPassAsync(id);

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
        public async Task<IActionResult> CreateUser([FromBody]User user)
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

                await _repository.User.CreateUserAsync(user);
 
                return Ok("Created");
                //return CreatedAtRoute("UserById", new { id = user.UserId}, user);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]Guid id, [FromBody]User user)
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

                var dbUser = await _repository.User.GetUserByIdAsync(id);
                if (dbUser.IsEmptyObject())
                {
                    return NotFound();
                }

                await _repository.User.UpdateUserAsync(dbUser, user);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]Guid id)
        {
            try
            {
                var user = await _repository.User.GetUserByIdAsync(id);
                if (user.IsEmptyObject())
                {
                    return NotFound();
                }
                var customer = await _repository.Customer.CustomersByUserAsync(id);
                //Modify for delete on cascade in database and remove this code
                if(customer.Any())
                {
                    return BadRequest("Cannot delete user. It has related customers created or updated. Delete those customers first");
                }
                await _repository.User.DeleteUserAsync(user);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
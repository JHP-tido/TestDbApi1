using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestDbApi.Interface;
using TestDbApi.Models;
using TestDbApi.Models.Extensions;

namespace TestDbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public LoginController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]Login login)
        {
            try{

                IActionResult response = Unauthorized();

                if (login == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var user = await _repository.Login.Authenticate(login);
                if (user.IsEmptyObject())
                {
                    return NotFound();
                }

                if (user != null)
                {
                    var tokenString = _repository.Login.BuildToken(user);
                    response = Ok(new { token = tokenString });
                }

                return response;
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
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
                Console.WriteLine("__________________Entrada1");
                if (login == null)
                {
                    Console.WriteLine("_______________Entrada2LoginNull");
                    return BadRequest("User object is null");
                }
                Console.WriteLine("__________________Entrada3LoginNotNull");
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("______________Entrada4ModelStateNoValid");
                    return BadRequest("Invalid model object");
                }
                Console.WriteLine("__________________Entrada5ModelStatedValid");
                var user = await _repository.Login.Authenticate(login);
                Console.WriteLine("_________________Entrada6EncuentraYDevuelveUser");
                Console.WriteLine("ValueUser: " + user);
                if (user.IsEmptyObject())
                {
                    Console.WriteLine("______________Entrada7UserIsEmptyObject");
                    return NotFound();
                }

                if (user != null)
                {
                    Console.WriteLine("____________Entrada8CreateToken");
                    var tokenString = _repository.Login.BuildToken(user);
                    Console.WriteLine("____________Entrada12TokenGet");
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
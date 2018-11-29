using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestDbApi.Interface;

namespace TestDbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;

        public ValuesController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            //var userRole = _repoWrapper.User.FindByCondition(x => x.Role.Equals();
            //var users = _repoWrapper.User.FindAll();
            var users = _repoWrapper.User.FindByCondition(x => x.Username.Equals("user1"));
            var customers = _repoWrapper.Customer.FindAll();
            //var customers = _repoWrapper.Customer.FindByCondition(x => x.CreatedBy.Username.Equals("user1"));
            return Ok(customers);
        }

        /*// GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

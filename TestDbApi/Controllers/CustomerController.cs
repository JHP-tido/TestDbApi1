using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestDbApi.Interface;

namespace TestDbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public CustomerController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _repository.Customer.GetAllCustomers();
                return Ok(customers);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/nouserinfo")]
        public IActionResult GetCustomerById(Guid id)
        {
            try
            {
                var customer = _repository.Customer.GetCustomerById(id);
 
                if (customer.CustomerId.Equals(Guid.Empty))
                { 
                    return NotFound();
                }
                else
                {
                    return Ok(customer);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerWithDetails(Guid id)
        {
            try
            {
                var customer = _repository.Customer.GetCustomerWithDetails(id);
 
                if (customer.CustomerId.Equals(Guid.Empty))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(customer);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/image")]
        public IActionResult GetCustomerImage(Guid id)
        {
            try
            {
                var imageUrl = _repository.Customer.GetCustomerImage(id);

                if (imageUrl.CustomerId.Equals(Guid.Empty))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(imageUrl);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
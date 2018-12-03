using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestDbApi.Interface;
using TestDbApi.Models.Extensions;

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
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _repository.Customer.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            try
            {
                var customer = await _repository.Customer.GetCustomerByIdAsync(id);
 
                if (customer.IsEmptyObject())
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

        [HttpGet("{id}/userid")]
        public async Task<IActionResult> GetCustomerWithUserId(Guid id)
        {
            try
            {
                var customer = await _repository.Customer.GetCustomersWithUsersIdAsync(id);

                if (customer.IsEmptyObject())
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

        [HttpGet("{id}/user")]
        public async Task<IActionResult> GetCustomerWithDetails(Guid id)
        {
            try
            {
                var customer = await _repository.Customer.GetCustomerWithDetailsAsync(id);
 
                if (customer.IsEmptyObject())
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
        public async Task<IActionResult> GetCustomerImage(Guid id)
        {
            try
            {
                var imageUrl = await _repository.Customer.GetCustomerImageAsync(id);

                if (imageUrl == null || imageUrl == "")
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestDbApi.Interface;
using TestDbApi.Models.Extensions;
using TestDbApi.Models;
using System.Security.Claims;

namespace TestDbApi.Controllers
{
    [Authorize(Roles = "admin, user")]
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

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody]Customer customer)
        {
            try
            {
                if (customer.IsObjectNull())
                {
                    return BadRequest("User object is null");
                }

                customer.CreatedById = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                customer.UpdatedById = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                //Will change in future for route in wwwroot where the images are stored
                customer.Image = null;
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                await _repository.Customer.CreateCustomerAsync(customer);

                return Ok("Created");
                //return CreatedAtRoute("UserById", new { id = user.UserId}, user);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
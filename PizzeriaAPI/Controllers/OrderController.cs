using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaAPI.Data;
using PizzeriaAPI.DTO;
using PizzeriaAPI.Interfaces;
using PizzeriaAPI.Models;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository, IPizzeriaRepository pizzeriaRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateOrder([FromBody] List<OrderDTO> orderPayloads)
        {
            if (orderPayloads == null)
                return BadRequest(ModelState);

            if (!await _orderRepository.CreateOrder(orderPayloads))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}

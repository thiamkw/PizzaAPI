using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaAPI.Data;
using PizzeriaAPI.DTO;
using PizzeriaAPI.Interfaces;
using PizzeriaAPI.Models;
using PizzeriaAPI.Repositories;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaRepository _pizzaRepository;

        private readonly IMapper _mapper;

        private IPizzeriaRepository _pizzeriaRepository;

        public PizzaController(IPizzaRepository pizzaRepository, IPizzeriaRepository pizzeriaRepository, IMapper mapper)
        {
            _pizzaRepository = pizzaRepository;
            _pizzeriaRepository = pizzeriaRepository;
            _mapper = mapper;
        }

        [HttpGet("Pizzeria/{pizzeriaId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PizzaDTO>))]
        public IActionResult GetPizzasByPizzeriaId(int pizzeriaId)
        {
            var pizzas = _mapper.Map<List<PizzaDTO>>(_pizzaRepository.GetPizzasByPizzeriaId(pizzeriaId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PizzaDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetPizza(int id)
        {
            if (!_pizzaRepository.PizzaExist(id))
                return NotFound();

            var pizza = _mapper.Map<PizzaDTO>(_pizzaRepository.GetPizza(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pizza);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePizza([FromQuery] int pizzeriaId, [FromBody] PizzaDTO pizzaCreate)
        {
            if (pizzaCreate == null)
                return BadRequest(ModelState);

            var pizzeria = _pizzeriaRepository.GerPizzeria(pizzeriaId);

            Pizza pizza = new Pizza()
            {
                Name = pizzaCreate.Name,
                Description = pizzaCreate.Description,
                BasePrice = pizzaCreate.BasePrice,
                IsActive = pizzaCreate.IsActive,
                PizzeriaId = pizzeriaId,
                Pizzeria = pizzeria,
            };

            if (!_pizzaRepository.CreatePizza(pizza))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePizza(int id,
            [FromBody] PizzaDTO updatePizza)
        {
            if (updatePizza == null)
                return BadRequest(ModelState);

            if (id != updatePizza.Id)
                return BadRequest(ModelState);

            if (!_pizzaRepository.PizzaExist(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var originPizza = _pizzaRepository.GetPizza(id);

            originPizza.Name = updatePizza.Name;
            originPizza.Description = updatePizza.Description;
            originPizza.BasePrice = updatePizza.BasePrice;
            originPizza.IsActive = updatePizza.IsActive;


            if (!_pizzaRepository.UpdatePizza(originPizza))
            {
                ModelState.AddModelError("", "Something went wrong updating pizza");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

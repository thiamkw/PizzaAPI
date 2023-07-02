using AutoMapper;
using Microsoft.AspNetCore.Cors;
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
    public class PizzeriaController : ControllerBase
    {
        private readonly IPizzeriaRepository _pizzeriaRepository;

        private readonly IMapper _mapper;

        public PizzeriaController(IPizzeriaRepository pizzeriaRepository, IMapper mapper)
        {
            _pizzeriaRepository = pizzeriaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PizzeriaDTO>))]
        public IActionResult GetPizzerias()
        {
            var pizzerias = _mapper.Map<List<PizzeriaDTO>>(_pizzeriaRepository.GerPizzerias());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pizzerias);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PizzeriaDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetPizzeria(int id)
        {
            if (!_pizzeriaRepository.PizzeriaExist(id))
                return NotFound();

            var pizzeria = _mapper.Map<PizzeriaDTO>(_pizzeriaRepository.GerPizzeria(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pizzeria);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePizzeria([FromBody] PizzeriaDTO pizzeriaCreate)
        {
            if (pizzeriaCreate == null)
                return BadRequest(ModelState);

            Pizzeria pizzeria = new Pizzeria()
            {
                Name = pizzeriaCreate.Name,
                Location = pizzeriaCreate.Location,
                IsActive = pizzeriaCreate.IsActive,
                Pizzas = new List<Pizza>(),
            };

            if (!_pizzeriaRepository.CreatePizzeria(pizzeria))
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
        public IActionResult UpdatePizzeria(int id,
            [FromBody] PizzeriaDTO updatePizzeria)
        {
            if (updatePizzeria == null)
                return BadRequest(ModelState);

            if (id != updatePizzeria.Id)
                return BadRequest(ModelState);

            if (!_pizzeriaRepository.PizzeriaExist(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var pizzeriaMap = _mapper.Map<Pizzeria>(updatePizzeria);

            if (!_pizzeriaRepository.UpdatePizzeria(pizzeriaMap))
            {
                ModelState.AddModelError("", "Something went wrong updating pizzeria");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}

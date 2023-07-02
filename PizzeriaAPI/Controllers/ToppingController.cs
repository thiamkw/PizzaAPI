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
    public class ToppingController : ControllerBase
    {
        private readonly IToppingRepository _toppingRepository;

        private readonly IMapper _mapper;

        private IPizzeriaRepository _pizzeriaRepository;

        public ToppingController(IToppingRepository toppingRepository, IPizzeriaRepository pizzeriaRepository, IMapper mapper)
        {
            _toppingRepository = toppingRepository;
            _pizzeriaRepository = pizzeriaRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ToppingDTO>))]
        public IActionResult GetTopping()
        {
            var toppings = _mapper.Map<List<ToppingDTO>>(_toppingRepository.GetToppings());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(toppings);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ToppingDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetPizza(int id)
        {
            if (!_toppingRepository.ToppingExist(id))
                return NotFound();

            var topping = _mapper.Map<ToppingDTO>(_toppingRepository.GetTopping(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(topping);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTopping([FromBody] ToppingDTO toppingCreate)
        {
            if (toppingCreate == null)
                return BadRequest(ModelState);

            Topping topping = new Topping()
            {
                Name = toppingCreate.Name,
                BasePrice = toppingCreate.BasePrice,
                IsActive = toppingCreate.IsActive,
            };

            if (!_toppingRepository.CreateTopping(topping))
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
        public IActionResult UpdateTopping(int id,
            [FromBody] ToppingDTO updateTopping)
        {
            if (updateTopping == null)
                return BadRequest(ModelState);

            if (id != updateTopping.Id)
                return BadRequest(ModelState);

            if (!_toppingRepository.ToppingExist(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var originTopping = _toppingRepository.GetTopping(id);

            originTopping.Name = updateTopping.Name;
            originTopping.BasePrice = updateTopping.BasePrice;
            originTopping.IsActive = updateTopping.IsActive;


            if (!_toppingRepository.UpdateTopping(originTopping))
            {
                ModelState.AddModelError("", "Something went wrong updating topping");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

using Xunit;
using Moq;
using PizzeriaAPI.Controllers;
using PizzeriaAPI.Interfaces;
using PizzeriaAPI.Models;
using PizzeriaAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace PizzeriaAPITests
{
    public class PizzaControllerTest
    {
        private readonly Mock<IPizzaRepository> _pizzaRepoMock = new Mock<IPizzaRepository>();
        private readonly Mock<IPizzeriaRepository> _pizzeriaRepoMock = new Mock<IPizzeriaRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        [Fact]
        public void GetPizzasByPizzeriaId_ReturnsOkResult()
        {
            var pizzaController = new PizzaController(_pizzaRepoMock.Object, _pizzeriaRepoMock.Object, _mapperMock.Object);
            _pizzaRepoMock.Setup(repo => repo.GetPizzasByPizzeriaId(It.IsAny<int>())).Returns(GetSamplePizzas());
            _mapperMock.Setup(m => m.Map<List<PizzaDTO>>(It.IsAny<List<Pizza>>())).Returns(GetSamplePizzaDTOs());

            var result = pizzaController.GetPizzasByPizzeriaId(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<PizzaDTO>>(okResult.Value);
            Assert.Equal(3, returnValue.Count);
        }


        [Fact]
        public void CreatePizza_ReturnsOkResult()
        {
            var pizzaController = new PizzaController(_pizzaRepoMock.Object, _pizzeriaRepoMock.Object, _mapperMock.Object);
            _pizzaRepoMock.Setup(repo => repo.CreatePizza(It.IsAny<Pizza>())).Returns(true);

            var result = pizzaController.CreatePizza(1, new PizzaDTO());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdatePizza_ReturnsNoContentResult()
        {
            var pizzaController = new PizzaController(_pizzaRepoMock.Object, _pizzeriaRepoMock.Object, _mapperMock.Object);
            _pizzaRepoMock.Setup(repo => repo.PizzaExist(It.IsAny<int>())).Returns(true);
            _pizzaRepoMock.Setup(repo => repo.GetPizza(It.IsAny<int>())).Returns(new Pizza());  // Setup a return value here
            _pizzaRepoMock.Setup(repo => repo.UpdatePizza(It.IsAny<Pizza>())).Returns(true);
            var updatePizzaDTO = new PizzaDTO { Id = 1 };

            var result = pizzaController.UpdatePizza(1, updatePizzaDTO);

            Assert.IsType<NoContentResult>(result);
        }



        private List<PizzaDTO> GetSamplePizzaDTOs()
        {
            return new List<PizzaDTO> { new PizzaDTO(), new PizzaDTO(), new PizzaDTO() };
        }

        private List<Pizza> GetSamplePizzas()
        {
            return new List<Pizza> { new Pizza(), new Pizza(), new Pizza() };
        }
    }
}

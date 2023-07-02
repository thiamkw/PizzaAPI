using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using PizzeriaAPI.Data;
using PizzeriaAPI.Models;
using PizzeriaAPI.Repositories;

namespace PizzeriaAPI.Tests
{
    public class PizzaRepositoryTests
    {
        [Fact]
        public void GetPizza_ReturnsPizza_WhenPizzaExists()
        {
            var data = new List<Pizza>
            {
                new Pizza { Id = 1, Name = "Pizza 1", PizzeriaId = 1 },
                new Pizza { Id = 2, Name = "Pizza 2", PizzeriaId = 2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Pizza>>();
            mockSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var configurationMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();

        
            var mockContextOptions = new DbContextOptionsBuilder<DataContext>()
                                        .UseInMemoryDatabase(databaseName: "TestDb")
                                        .Options;

            using (var context = new DataContext(mockContextOptions, configurationMock.Object))
            {
                context.Pizzas = mockSet.Object;
                var repository = new PizzaRepository(context);

                var pizza = repository.GetPizza(1);

                Assert.NotNull(pizza);
                Assert.Equal(1, pizza.Id);
            }
        }

        [Fact]
        public void GetPizzasByPizzeriaId_ReturnsPizzas_WhenPizzasExist()
        {
            var data = new List<Pizza>
            {
                new Pizza { Id = 1, Name = "Pizza 1", PizzeriaId = 1 },
                new Pizza { Id = 2, Name = "Pizza 2", PizzeriaId = 1 },
                new Pizza { Id = 3, Name = "Pizza 3", PizzeriaId = 2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Pizza>>();
            mockSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var configurationMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();

            var mockContextOptions = new DbContextOptionsBuilder<DataContext>()
                                        .UseInMemoryDatabase(databaseName: "TestDb")
                                        .Options;

            using (var context = new DataContext(mockContextOptions, configurationMock.Object))
            {
                context.Pizzas = mockSet.Object;
                var repository = new PizzaRepository(context);

                var pizzas = repository.GetPizzasByPizzeriaId(1);

                Assert.NotNull(pizzas);
                Assert.Equal(2, pizzas.Count);
            }
        }

    }
}

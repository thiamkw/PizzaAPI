using Microsoft.EntityFrameworkCore;
using PizzeriaAPI.Data;
using PizzeriaAPI.DTO;
using PizzeriaAPI.Interfaces;
using PizzeriaAPI.Models;

namespace PizzeriaAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;

        public OrderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateOrder(List<OrderDTO> orderPayloads)
        {
            using (var transaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var orderPayload in orderPayloads)
                    {
                        // Create new order
                        var order = new Order
                        {
                            PizzeriaId = orderPayload.PizzeriaId,
                            TotalPrice = orderPayload.TotalPrice
                        };

                        // Save order to get its Id
                        _dataContext.Orders.Add(order);
                        await _dataContext.SaveChangesAsync();

                        // Create new order item
                        var orderItem = new OrderItem
                        {
                            OrderId = order.Id
                        };

                        // Save order item to get its Id
                        _dataContext.OrderItems.Add(orderItem);
                        await _dataContext.SaveChangesAsync();

                        // Create new order item pizza
                        var orderItemPizza = new OrderItemPizza
                        {
                            OrderItemId = orderItem.OrderId,
                            PizzaId = orderPayload.Pizzas.Id
                        };
                        _dataContext.OrderItemPizzas.Add(orderItemPizza);

                        // Create new order item toppings
                        foreach (var topping in orderPayload.Toppings)
                        {
                            var orderItemTopping = new OrderItemTopping
                            {
                                OrderItemId = orderItem.OrderId,
                                ToppingId = topping.Id
                            };
                            _dataContext.OrderItemToppings.Add(orderItemTopping);
                        }
                        await _dataContext.SaveChangesAsync();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // If there's an error, roll back the transaction
                    transaction.Rollback();
                    throw;
                }
            }
            return true;
        }


    }
}

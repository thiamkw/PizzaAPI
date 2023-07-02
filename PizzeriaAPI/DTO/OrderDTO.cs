using PizzeriaAPI.Models;
using System.Collections.ObjectModel;

namespace PizzeriaAPI.DTO
{
    public class OrderDTO
    {
        public int PizzeriaId { get; set; }
        public PizzaDTO Pizzas { get; set; }
        public List<ToppingDTO> Toppings { get; set; }
        public double TotalPrice { get; set; }
    }
}

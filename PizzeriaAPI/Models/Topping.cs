using System.Collections.ObjectModel;

namespace PizzeriaAPI.Models
{
    public class Topping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public bool IsActive { get; set; }
        public ICollection<OrderItemTopping> OrderItemToppings { get; set; }
    }
}

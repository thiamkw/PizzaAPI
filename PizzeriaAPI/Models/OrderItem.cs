namespace PizzeriaAPI.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public ICollection<OrderItemPizza> OrderItemPizzas { get; set; }
        public ICollection<OrderItemTopping> OrderItemToppings { get; set; }
    }
}

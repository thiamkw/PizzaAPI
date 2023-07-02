namespace PizzeriaAPI.Models
{
    public class OrderItemPizza
    {
        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
    }
}

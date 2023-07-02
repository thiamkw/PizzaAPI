namespace PizzeriaAPI.Models
{
    public class OrderItemTopping
    {
        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
        public int ToppingId { get; set; }
        public Topping Topping { get; set; }
    }
}

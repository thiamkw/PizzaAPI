namespace PizzeriaAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int PizzeriaId { get; set; }
        public Pizzeria Pizzeria { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public double TotalPrice { get; set; }
    }
}

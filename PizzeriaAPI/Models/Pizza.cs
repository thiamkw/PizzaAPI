namespace PizzeriaAPI.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }
        public int PizzeriaId { get; set; }

        public bool IsActive { get; set; }
        public Pizzeria Pizzeria { get; set; }
        public ICollection<OrderItemPizza> OrderItemPizzas { get; set; }

    }
}

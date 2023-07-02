namespace PizzeriaAPI.Models
{
    public class Pizzeria
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public bool IsActive { get; set; }
        public ICollection<Pizza> Pizzas { get; set; }
    }
}

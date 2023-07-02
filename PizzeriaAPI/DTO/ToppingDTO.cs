using PizzeriaAPI.Models;

namespace PizzeriaAPI.DTO
{
    public class ToppingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public bool IsActive { get; set; }
    }
}

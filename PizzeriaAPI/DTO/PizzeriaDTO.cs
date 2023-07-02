using PizzeriaAPI.Models;

namespace PizzeriaAPI.DTO
{
    public class PizzeriaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}

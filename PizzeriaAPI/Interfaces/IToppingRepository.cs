using PizzeriaAPI.Models;

namespace PizzeriaAPI.Interfaces
{
    public interface IToppingRepository
    {
        ICollection<Topping> GetToppings();
        Topping GetTopping(int id);
        bool CreateTopping(Topping topping);
        bool UpdateTopping(Topping topping);
        bool ToppingExist(int id);
        public void Save();
    }
}

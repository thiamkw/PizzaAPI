using PizzeriaAPI.Models;

namespace PizzeriaAPI.Interfaces
{
    public interface IPizzaRepository
    {
        ICollection<Pizza> GetPizzasByPizzeriaId(int id);
        Pizza GetPizza(int id);
        bool CreatePizza(Pizza pizza);

        bool UpdatePizza(Pizza pizza);
        bool PizzaExist(int id);
        void Save();
    }
}

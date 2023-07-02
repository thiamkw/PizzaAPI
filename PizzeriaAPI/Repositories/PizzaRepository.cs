using PizzeriaAPI.Data;
using PizzeriaAPI.Interfaces;
using PizzeriaAPI.Models;

namespace PizzeriaAPI.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly DataContext _dataContext;

        public PizzaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Pizza> GetPizzasByPizzeriaId(int id)
        {
            return _dataContext.Pizzas.Where(p => p.PizzeriaId == id).ToList();
        }

        public Pizza GetPizza (int id)
        {
            return _dataContext.Pizzas.FirstOrDefault(p => p.Id == id);
        }


        public bool CreatePizza(Pizza pizza)
        {
            _dataContext.Add(pizza);
            try
            {
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePizza(Pizza pizza)
        {
            _dataContext.Update(pizza);
            try
            {
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool PizzaExist(int id)
        {
            return _dataContext.Pizzas.Any(p => p.Id == id);
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
    
    }
}

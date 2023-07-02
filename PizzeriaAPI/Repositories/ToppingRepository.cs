using PizzeriaAPI.Data;
using PizzeriaAPI.Interfaces;
using PizzeriaAPI.Models;

namespace PizzeriaAPI.Repositories
{
    public class ToppingRepository : IToppingRepository
    {
        private readonly DataContext _dataContext;

        public ToppingRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Topping> GetToppings()
        {
            return _dataContext.Toppings.ToList();
        }

        public Topping GetTopping(int id)
        {
            return _dataContext.Toppings.FirstOrDefault(t => t.Id == id);
        }


        public bool CreateTopping(Topping topping)
        {
            _dataContext.Add(topping);
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

        public bool UpdateTopping(Topping topping)
        {
            _dataContext.Update(topping);
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

        public bool ToppingExist(int id)
        {
            return _dataContext.Toppings.Any(t => t.Id == id);
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}

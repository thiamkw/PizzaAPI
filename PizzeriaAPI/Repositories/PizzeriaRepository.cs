using Microsoft.EntityFrameworkCore;
using PizzeriaAPI.Data;
using PizzeriaAPI.DTO;
using PizzeriaAPI.Interfaces;
using PizzeriaAPI.Models;

namespace PizzeriaAPI.Repositories
{
    public class PizzeriaRepository : IPizzeriaRepository
    {
        private readonly DataContext _dataContext;

        public PizzeriaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Pizzeria> GerPizzerias()
        {
            return _dataContext.Pizzerias.OrderBy(p => p.Id).ToList();
        }

        public Pizzeria GerPizzeria(int id)
        {
            return _dataContext.Pizzerias.FirstOrDefault(p => p.Id == id);
        }


        public bool CreatePizzeria(Pizzeria pizzeria)
        {
            _dataContext.Add(pizzeria);
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

        public bool UpdatePizzeria(Pizzeria pizzeria)
        {
            _dataContext.Update(pizzeria);
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


        public bool PizzeriaExist(int id)
        {
            return _dataContext.Pizzerias.Any(p => p.Id == id);
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}

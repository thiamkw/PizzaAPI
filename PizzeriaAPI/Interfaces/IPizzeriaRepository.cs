using PizzeriaAPI.Models;

namespace PizzeriaAPI.Interfaces
{
    public interface IPizzeriaRepository
    {
        ICollection<Pizzeria> GerPizzerias();
        Pizzeria GerPizzeria(int id);
        bool CreatePizzeria(Pizzeria pizzeria);

        bool UpdatePizzeria(Pizzeria pizzeria);

        bool PizzeriaExist(int id);

        void Save();
    }
}

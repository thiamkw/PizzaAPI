using AutoMapper;
using PizzeriaAPI.DTO;
using PizzeriaAPI.Models;

namespace PizzeriaAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pizzeria, PizzeriaDTO>();
            CreateMap<PizzeriaDTO, Pizzeria>();
            CreateMap<Pizza, PizzaDTO>();
            CreateMap<PizzaDTO, Pizza>();
            CreateMap<ToppingDTO, Topping>();
            CreateMap<Topping, ToppingDTO>();
        }
    }
}

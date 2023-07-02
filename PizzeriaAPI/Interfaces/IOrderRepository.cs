using PizzeriaAPI.DTO;

namespace PizzeriaAPI.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrder(List<OrderDTO> orderPayloads);
    }
}

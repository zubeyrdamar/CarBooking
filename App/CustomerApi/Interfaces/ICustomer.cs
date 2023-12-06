using CustomerApi.Models;

namespace CustomerApi.Interfaces
{
    public interface ICustomer
    {

        Task Create(Customer customer);
    }
}

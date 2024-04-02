using BankAccountAPI.Models;

namespace BankAccountAPI.Service.interfaces
{
    public interface IBankQueryService
    {

        Task<List<BankAccount>> GetAll();

        Task<BankAccount> GetById(int id);

        Task<List<BankAccount>> GetAllByName(string name);

    }
}

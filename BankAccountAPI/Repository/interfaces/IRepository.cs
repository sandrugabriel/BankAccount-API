using BankAccountAPI.Models;
using System;

namespace BankAccountAPI.Repository.interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<BankAccount>> GetAllAsync();

        Task<List<BankAccount>> GetByOwnerName(string name);

        Task<BankAccount> GetByIdAsync(int id);
    }
}

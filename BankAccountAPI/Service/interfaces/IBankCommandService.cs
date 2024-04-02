using BankAccountAPI.Dto;
using BankAccountAPI.Models;

namespace BankAccountAPI.Service.interfaces
{
    public interface IBankCommandService
    {
        Task<BankAccount> Create(CreateBankRequest request);

        Task<BankAccount> Update(int id, UpdateBankRequest request);

        Task<BankAccount> Delete(int id);

    }
}

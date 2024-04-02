using BankAccountAPI.Dto;
using BankAccountAPI.Exceptions;
using BankAccountAPI.Models;
using BankAccountAPI.Repository.interfaces;
using BankAccountAPI.Service.interfaces;

namespace BankAccountAPI.Service
{
    public class BankCommandService : IBankCommandService
    {


        private IRepository _repository;

        public BankCommandService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<BankAccount> Create(CreateBankRequest request)
        {
            if (request.Balance <= -1)
            {
                throw new InvalidPrice(Constants.Constants.InvalidPrice);
            }

            var bank = await _repository.Create(request);

            return bank;
        }

        public async Task<BankAccount> Delete(int id)
        {

            var bank = await _repository.GetByIdAsync(id);
            if (bank == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }
            bank = await _repository.DeleteById(id);
            return bank;
        }

        public async Task<BankAccount> Update(int id, UpdateBankRequest request)
        {

            var bank = await _repository.GetByIdAsync(id);
            if (bank == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }


            if (bank.Balance <= -1)
            {
                throw new InvalidPrice(Constants.Constants.InvalidPrice);
            }
            bank = await _repository.Update(id, request);
            return bank;
        }
    }
}

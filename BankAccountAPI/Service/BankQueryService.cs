using BankAccountAPI.Exceptions;
using BankAccountAPI.Models;
using BankAccountAPI.Repository.interfaces;
using BankAccountAPI.Service.interfaces;
using BankAccountAPI.Exceptions;

namespace BankAccountAPI.Service
{
    public class BankQueryService : IBankQueryService
    {
        private IRepository _repository;

        public BankQueryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BankAccount>> GetAll()
        {
            var bank = await _repository.GetAllAsync();

            if (bank.Count() == 0)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemsDoNotExist);
            }

            return bank;
        }

        public async Task<List<BankAccount>> GetAllByName(string name)
        {
            var bank = await _repository.GetByOwnerName(name);

            if (bank.Count == 0)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemsDoNotExist);
            }

            return bank;

        }

        public async Task<BankAccount> GetById(int id)
        {
            var apartament = await _repository.GetByIdAsync(id);

            if (apartament == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return apartament;
        }
    }
}

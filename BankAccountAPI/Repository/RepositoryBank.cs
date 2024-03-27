using AutoMapper;
using BankAccountAPI.Data;
using BankAccountAPI.Dto;
using BankAccountAPI.Models;
using BankAccountAPI.Repository.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace BankAccountAPI.Repository
{
    public class RepositoryBank : IRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public RepositoryBank(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankAccount>> GetAllAsync()
        {
            return await _context.BankAccounts.ToListAsync();
        }

        public async Task<BankAccount> GetByIdAsync(int id)
        {
            List<BankAccount> bankAccounts = await _context.BankAccounts.ToListAsync();

            for (int i = 0; i < bankAccounts.Count; i++)
            {
                if (bankAccounts[i].Id == id) return bankAccounts[i];
            }

            return null;
        }

        public async Task<List<BankAccount>> GetByOwnerName(string name)
        {
            List<BankAccount> bankAccounts = await _context.BankAccounts.ToListAsync();

            List<BankAccount> myAccount = new List<BankAccount>();
            for(int i=0;i<bankAccounts.Count;i++)
            {
                if (bankAccounts[i].Name.Equals(name)) myAccount.Add(bankAccounts[i]);
            }

            return myAccount;
        }

        public async Task<BankAccount> Create(CreateBankRequest request)
        {

            var bank = _mapper.Map<BankAccount>(request);

            _context.BankAccounts.Add(bank);

            await _context.SaveChangesAsync();

            return bank;

        }

        public async Task<BankAccount> Update(int id, UpdateBankRequest request)
        {

            var bank = await _context.BankAccounts.FindAsync(id);

            bank.Balance = request.Balance ?? bank.Balance;
            bank.Type = request.Type ?? bank.Type;
            bank.Name = request.Name ?? bank.Name;

            _context.BankAccounts.Update(bank);

            await _context.SaveChangesAsync();

            return bank;

        }

        public async Task<BankAccount> DeleteById(int id)
        {
            var bank = await _context.BankAccounts.FindAsync(id);

            _context.BankAccounts.Remove(bank);

            await _context.SaveChangesAsync();

            return bank;
        }




    }
}

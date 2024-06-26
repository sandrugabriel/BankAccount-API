﻿using BankAccountAPI.Dto;
using BankAccountAPI.Models;
using System;

namespace BankAccountAPI.Repository.interfaces
{
    public interface IRepository
    {
        Task<List<BankAccount>> GetAllAsync();

        Task<List<BankAccount>> GetByOwnerName(string name);

        Task<BankAccount> GetByIdAsync(int id);

        Task<BankAccount> Create(CreateBankRequest request);

        Task<BankAccount> Update(int id, UpdateBankRequest request);

        Task<BankAccount> DeleteById(int id);


    }
}

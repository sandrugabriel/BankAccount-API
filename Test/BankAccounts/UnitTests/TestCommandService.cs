using BankAccountAPI.Constants;
using BankAccountAPI.Dto;
using BankAccountAPI.Exceptions;
using BankAccountAPI.Models;
using BankAccountAPI.Repository.interfaces;
using BankAccountAPI.Service;
using BankAccountAPI.Service.interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.BankAccounts.Helpers;

namespace Test.BankAccounts.UnitTests
{
    public class TestCommandService
    {
        private readonly Mock<IRepository> _mock;
        private readonly IBankCommandService _commandService;

        public TestCommandService()
        {
            _mock = new Mock<IRepository>();
            _commandService = new BankCommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidPrice()
        {
            var createRequest = new CreateBankRequest
            {
                Balance = -1,
                Name = "gabi",
                Type = "salary"
            };

            _mock.Setup(repo => repo.Create(createRequest)).ReturnsAsync((BankAccount)null);
            var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _commandService.Create(createRequest));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task Create_ValidData()
        {
            var createRequest = new CreateBankRequest
            {
                Balance = 1000,
                Name = "gabi",
                Type = "salary"
            };

            var bankAccount = TestBankAccountFactory.CreateBankAccount(50);
            bankAccount.Balance = createRequest.Balance;

            _mock.Setup(repo => repo.Create(It.IsAny<CreateBankRequest>())).ReturnsAsync(bankAccount);

            var result = await _commandService.Create(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.Balance, createRequest.Balance);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateBankRequest
            {
                Balance = 100000
            };

            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((BankAccount)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.Update(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidPrice()
        {
            var updateRequest = new UpdateBankRequest
            {
                Balance = -1
            };
            var bankAccount = TestBankAccountFactory.CreateBankAccount(50);
            bankAccount.Balance = updateRequest.Balance.Value;
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync(bankAccount);

            var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _commandService.Update(50, updateRequest));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData()
        {
            var updateREquest = new UpdateBankRequest
            {
                Balance = 1000000
            };

            var bankAccount = TestBankAccountFactory.CreateBankAccount(1);
            bankAccount.Balance = updateREquest.Balance.Value;

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(bankAccount);
            _mock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateBankRequest>())).ReturnsAsync(bankAccount);

            var result = await _commandService.Update(1, updateREquest);

            Assert.NotNull(result);
            Assert.Equal(bankAccount, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync((BankAccount)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.Delete(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            BankAccount bankAccount = TestBankAccountFactory.CreateBankAccount(50);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(bankAccount);

            var restul = await _commandService.Delete(50);

            Assert.NotNull(restul);
            Assert.Equal(bankAccount, restul);
        }

    }
}

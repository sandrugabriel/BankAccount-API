using BankAccountAPI.Constants;
using BankAccountAPI.Exceptions;
using BankAccountAPI.Models;
using BankAccountAPI.Repository.interfaces;
using BankAccountAPI.Service;
using BankAccountAPI.Service.interfaces;
using Moq;
using Test.BankAccounts.Helpers;

namespace Test.BankAccounts.UnitTests
{
    public class TestQueryService
    {
        private readonly Mock<IRepository> _mock;
        private readonly IBankQueryService _service;

        public TestQueryService()
        {
            _mock = new Mock<IRepository>();
            _service = new BankQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<BankAccount>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAll());

            Assert.Equal(exception.Message, Constants.ItemsDoNotExist);
        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var banks = TestBankAccountFactory.CreateBankAccounts(5);

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(banks);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Equal(banks, result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((BankAccount)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);
        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var bank = TestBankAccountFactory.CreateBankAccount(1);
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(bank);

            var result = await _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(bank, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByOwnerName("")).ReturnsAsync(new List<BankAccount>());
            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllByName(""));

            Assert.Equal(Constants.ItemsDoNotExist, exception.Message);
        }

        [Fact]
        public async Task GetByName_ValidData()
        {
            var bank = TestBankAccountFactory.CreateBankAccounts(10);
            _mock.Setup(repo => repo.GetByOwnerName("test")).ReturnsAsync(bank);
            var result = await _service.GetAllByName("test");

            Assert.NotNull(result);
            Assert.Equal(bank, result);

        }

    }
}

using BankAccountAPI.Constants;
using BankAccountAPI.Controllers;
using BankAccountAPI.Controllers.interfaces;
using BankAccountAPI.Dto;
using BankAccountAPI.Exceptions;
using BankAccountAPI.Models;
using BankAccountAPI.Service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.BankAccounts.Helpers;

namespace Test.BankAccounts.UnitTests
{
    public class TestController
    {

        private readonly Mock<IBankCommandService> _mockCommandService;
        private readonly Mock<IBankQueryService> _mockQueryService;
        private readonly BankAPIController bankAccountApiController;

        public TestController()
        {
            _mockCommandService = new Mock<IBankCommandService>();
            _mockQueryService = new Mock<IBankQueryService>();

            bankAccountApiController = new ControllerBank(_mockQueryService.Object, _mockCommandService.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));

            var restult = await bankAccountApiController.GetAll();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemsDoNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var bankAccounts = TestBankAccountFactory.CreateBankAccounts(5);
            _mockQueryService.Setup(repo => repo.GetAll()).ReturnsAsync(bankAccounts);

            var result = await bankAccountApiController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allbankAccounts = Assert.IsType<List<BankAccount>>(okResult.Value);

            Assert.Equal(4, allbankAccounts.Count);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidPrice()
        {

            var createRequest = new CreateBankRequest
            {
                Balance = 0,
                Name = "gabi",
                Type = "salary"
            };

            _mockCommandService.Setup(repo => repo.Create(It.IsAny<CreateBankRequest>())).
                ThrowsAsync(new InvalidPrice(Constants.InvalidPrice));

            var result = await bankAccountApiController.CreateBankAccount(createRequest);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, badRequest.StatusCode);
            Assert.Equal(Constants.InvalidPrice, badRequest.Value);

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

            var bankAccount = TestBankAccountFactory.CreateBankAccount(1);
            bankAccount.Balance = createRequest.Balance;
            bankAccount.Name = createRequest.Name;
            bankAccount.Type = createRequest.Type;

            _mockCommandService.Setup(repo => repo.Create(It.IsAny<CreateBankRequest>())).ReturnsAsync(bankAccount);

            var result = await bankAccountApiController.CreateBankAccount(createRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, bankAccount);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateBankRequest
            {
                Balance = 0
            };

            _mockCommandService.Setup(repo => repo.Update(1, update)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await bankAccountApiController.UpdateBankAccount(1, update);

            var ntFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(ntFound.StatusCode, 404);
            Assert.Equal(Constants.ItemDoesNotExist, ntFound.Value);

        }
        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateBankRequest
            {
                Balance = 10000
            };

            var bankAccount = TestBankAccountFactory.CreateBankAccount(1);

            _mockCommandService.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateBankRequest>())).ReturnsAsync(bankAccount);

            var result = await bankAccountApiController.UpdateBankAccount(1, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, bankAccount);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mockCommandService.Setup(repo => repo.Delete(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await bankAccountApiController.DeleteBankAccount(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var bankAccount = TestBankAccountFactory.CreateBankAccount(1);

            _mockCommandService.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(bankAccount);

            var result = await bankAccountApiController.DeleteBankAccount(1);

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.Equal(okresult.Value, bankAccount);

        }
    }
}

using System.Net;
using System.Text;
using BankAccountAPI.Dto;
using BankAccountAPI.Models;
using Newtonsoft.Json;
using Test.BankAccounts.Helpers;
using Test.BankAccounts.Intrastructure;

namespace Test.BankAccounts.IntegrationTests;

public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
{
    
        private readonly HttpClient _client;

        public IntegrationTest(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllBankAccounts_BankAccountsFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createBankAccountRequest = TestBankAccountFactory.CreateBankAccount(1);
            var content = new StringContent(JsonConvert.SerializeObject(createBankAccountRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerBank/createBankAccount", content);

            var response = await _client.GetAsync("/api/v1/ControllerBank/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBankAccountById_BankAccountFound_ReturnsOkStatusCode()
        {
            var createBankAccountRequest = new CreateBankRequest
            { Balance = 30, Type = "ASasdadd", Name = "test"};
            var content = new StringContent(JsonConvert.SerializeObject(createBankAccountRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/ControllerBank/createBankAccount", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BankAccount>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Name, createBankAccountRequest.Name);
        }

        [Fact]
        public async Task GetBankAccountById_BankAccountNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerBank/findById?id=9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerBank/createBankAccount";
            var createBankAccount = new CreateBankRequest
                { Balance = 30, Type = "ASasdadd", Name = "test"};

            var content = new StringContent(JsonConvert.SerializeObject(createBankAccount), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BankAccount>(responseString);

            Assert.NotNull(result);
            Assert.Equal(createBankAccount.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerBank/createBankAccount";
            var createBankAccount = new CreateBankRequest
                { Balance = 30, Type = "ASasdadd", Name = "test"};

            var content = new StringContent(JsonConvert.SerializeObject(createBankAccount), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BankAccount>(responseString);

            request = $"/api/v1/ControllerBank/updateBankAccount?id={result.Id}";
            var updateBankAccount = new UpdateBankRequest { Balance = 20 };
            content = new StringContent(JsonConvert.SerializeObject(updateBankAccount), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<BankAccount>(responceStringUp);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Balance, updateBankAccount.Balance);
        }

        [Fact]
        public async Task Put_Update_InvalidBankAccountPrice_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerBank/createBankAccount";
            var createBankAccount = new CreateBankRequest
                { Balance = 30, Type = "ASasdadd", Name = "test"};

            var content = new StringContent(JsonConvert.SerializeObject(createBankAccount), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BankAccount>(responseString);

            request = $"/api/v1/ControllerBank/updateBankAccount?id={result.Id}";
            var updateBankAccount = new UpdateBankRequest { Balance = -3 };
            content = new StringContent(JsonConvert.SerializeObject(updateBankAccount), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<BankAccount>(responseString);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result1.Balance, updateBankAccount.Balance);
        }

        [Fact]
        public async Task Put_Update_BankAccountDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerBank/updateBankAccount";
            var updateBankAccount = new UpdateBankRequest { Balance = 30 };
            var content = new StringContent(JsonConvert.SerializeObject(updateBankAccount), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_BankAccountExists_ReturnsDeletedBankAccount()
        {
            var request = "/api/v1/ControllerBank/createBankAccount";
            var createBankAccount = new CreateBankRequest
                { Balance = 30, Type = "ASasdadd", Name = "test"};

            var content = new StringContent(JsonConvert.SerializeObject(createBankAccount), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BankAccount>(responseString)!;

            request = $"/api/v1/ControllerBank/deleteBankAccount?id={result.Id}";

            response = await _client.DeleteAsync(request);
            var responceString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<BankAccount>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Name, createBankAccount.Name);
        }

        [Fact]
        public async Task Delete_Delete_BankAccountDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerBank/deleteBankAccount?id=7";

            var response = await _client.DeleteAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

}
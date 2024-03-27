using BankAccountAPI.Dto;
using BankAccountAPI.Models;
using BankAccountAPI.Repository.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/v1/bankAccounts")]
    public class ControllerBank : ControllerBase
    {

        private readonly ILogger<ControllerBank> _logger;

        private IRepository _repository;

        public ControllerBank(ILogger<ControllerBank> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("/all")]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetAll()
        {
            var bank = await _repository.GetAllAsync();
            return Ok(bank);
        }

        [HttpGet("/findById")]
        public async Task<ActionResult<BankAccount>> GetById([FromQuery]int id)
        {
            var bank = await _repository.GetByIdAsync(id);
            return Ok(bank);
        }

        [HttpGet("/findByName/{name}")]
        public async Task<ActionResult<List<BankAccount>>> GetByName([FromRoute] string name)
        {
            var bank = await _repository.GetByOwnerName(name);
            return Ok(bank);
        }


        [HttpPost("/create")]
        public async Task<ActionResult<BankAccount>> CreateCar([FromBody] CreateBankRequest request)
        {
            var car = await _repository.Create(request);
            return Ok(car);

        }

        [HttpPut("/update")]
        public async Task<ActionResult<BankAccount>> UpdateCar([FromQuery] int id, [FromBody] UpdateBankRequest request)
        {
            var car = await _repository.Update(id, request);
            return Ok(car);
        }

        [HttpDelete("/deleteById")]
        public async Task<ActionResult<BankAccount>> DeleteCarById([FromQuery] int id)
        {
            var car = await _repository.DeleteById(id);
            return Ok(car);
        }

    }
}

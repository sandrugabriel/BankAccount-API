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
    }
}

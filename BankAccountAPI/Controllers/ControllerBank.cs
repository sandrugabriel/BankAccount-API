using BankAccountAPI.Controllers.interfaces;
using BankAccountAPI.Dto;
using BankAccountAPI.Exceptions;
using BankAccountAPI.Models;
using BankAccountAPI.Repository.interfaces;
using BankAccountAPI.Service.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankAccountAPI.Controllers
{
    public class ControllerBank : BankAPIController
    {

        private IBankQueryService _queryService;
        private IBankCommandService _commandService;

        public ControllerBank(IBankQueryService queryService, IBankCommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        public override async Task<ActionResult<List<BankAccount>>> GetAll()
        {
            try
            {
                var banks = await _queryService.GetAll();

                return Ok(banks);

            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<List<BankAccount>>> GetAllByName([FromQuery]string name)
        {
            try
            {
                var banks = await _queryService.GetAllByName(name);
                return Ok(banks);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<BankAccount>> GetById(int id)
        {

            try
            {
                var bank = await _queryService.GetById(id);
                return Ok(bank);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<BankAccount>> CreateBankAccount(CreateBankRequest request)
        {
            try
            {
                var bank = await _commandService.Create(request);
                return Ok(bank);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<BankAccount>> UpdateBankAccount(int id, UpdateBankRequest request)
        {
            try
            {
                var bank = await _commandService.Update(id, request);
                return Ok(bank);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<BankAccount>> DeleteBankAccount(int id)
        {
            try
            {
                var bank = await _commandService.Delete(id);
                return Ok(bank);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }




        /*
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
                public async Task<ActionResult<BankAccount>> Create([FromBody] CreateBankRequest request)
                {
                    var bank = await _repository.Create(request);
                    return Ok(bank);

                }

                [HttpPut("/update")]
                public async Task<ActionResult<BankAccount>> Update([FromQuery] int id, [FromBody] UpdateBankRequest request)
                {
                    var bank = await _repository.Update(id, request);
                    return Ok(bank);
                }

                [HttpDelete("/deleteById")]
                public async Task<ActionResult<BankAccount>> DeleteById([FromQuery] int id)
                {
                    var bank = await _repository.DeleteById(id);
                    return Ok(bank);
                }
        */
    }
}

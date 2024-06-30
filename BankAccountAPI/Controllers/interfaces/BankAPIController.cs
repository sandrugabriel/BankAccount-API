using BankAccountAPI.Dto;
using BankAccountAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class BankAPIController : ControllerBase
    {

        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<BankAccount>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<BankAccount>>> GetAll();

        [HttpGet("findByPrice")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<BankAccount>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<BankAccount>>> GetAllByName([FromQuery] string name);

        [HttpGet("findById")]
        [ProducesResponseType(statusCode: 200, type: typeof(BankAccount))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<BankAccount>> GetById(int id);

        [HttpPost("createBankAccount")]
        [ProducesResponseType(statusCode: 201, type: typeof(BankAccount))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<BankAccount>> CreateBankAccount(CreateBankRequest request);

        [HttpPut("updateBankAccount")]
        [ProducesResponseType(statusCode: 200, type: typeof(BankAccount))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<BankAccount>> UpdateBankAccount(int id, UpdateBankRequest request);

        [HttpDelete("deleteBankAccount")]
        [ProducesResponseType(statusCode: 200, type: typeof(BankAccount))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<BankAccount>> DeleteBankAccount(int id);

    }
}

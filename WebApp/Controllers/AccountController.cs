using Domain.Entities;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(AccountService service) : ControllerBase
{
    [HttpGet("GetAccounts")]
    public ApiResponse<List<Account>> GetAccounts()
    {
        return service.GetAll();
    }

    [HttpGet("GetAccount/{id}")]
    public ApiResponse<Account> GetAccount(int id)
    {
        return service.GetById(id);
    }

    [HttpPost("CreateAccount")]
    public ApiResponse<bool> CreateAccount(Account account)
    {
        return service.Add(account);
    }

    [HttpPut("UpdateAccount")]
    public ApiResponse<bool> UpdateAccount(Account account)
    {
        return service.Update(account);
    }

    [HttpDelete("DeleteAccount/{id}")]
    public ApiResponse<bool> DeleteAccount(int id)
    {
        return service.Delete(id);
    }

    [HttpPut("ReplenishBalance/{id}")]
    public ApiResponse<bool> ReplenishBalance(int id, decimal amount)
    {
        return service.ReplenishBalance(id, amount);
    }

    [HttpPut("WithdrawnMony/{id}")]
    public ApiResponse<bool> WithdrawnMony(int id,decimal mony)
    {
        return service.WithdrawnMony(id, mony);
    }
}
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
    public async Task<ApiResponse<List<Account>>> GetAccounts()
    {
        return await service.GetAll();
    }

    [HttpGet("GetAccount/{id}")]
    public async Task<ApiResponse<Account>> GetAccount(int id)
    {
        return await service.GetById(id);
    }

    [HttpPost("CreateAccount")]
    public async Task<ApiResponse<bool>> CreateAccount(Account account)
    {
        return await service.Add(account);
    }

    [HttpPut("UpdateAccount")]
    public async Task<ApiResponse<bool>> UpdateAccount(Account account)
    {
        return await service.Update(account);
    }

    [HttpDelete("DeleteAccount/{id}")]
    public async Task<ApiResponse<bool>> DeleteAccount(int id)
    {
        return await service.Delete(id);
    }

    [HttpPut("ReplenishBalance/{id}")]
    public async Task<ApiResponse<bool>> ReplenishBalance(int id, decimal amount)
    {
        return await service.ReplenishBalance(id, amount);
    }

    [HttpPut("WithdrawnMony/{id}")]
    public async Task<ApiResponse<bool>> WithdrawnMony(int id,decimal mony)
    {
        return await service.WithdrawnMony(id, mony);
    }
}
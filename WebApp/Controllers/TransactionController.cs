using Domain.Entities;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController(TransactionService transactionService):ControllerBase
{
    [HttpGet("GetTransactions")]
    public async Task<ApiResponse<List<Transaction>>> GetTransactions()
    {
        return await transactionService.GetAll();
    }

    [HttpGet("GetTransactions/{id}")]
    public async Task<ApiResponse<Transaction>> GetTransaction(int id)
    {
        return await transactionService.GetById(id);
    }

    [HttpPost("CreateTransaction")]
    public async Task<ApiResponse<bool>> CreateTransaction([FromBody] Transaction transaction)
    {
        return await  transactionService.Add(transaction);
    }

    [HttpPut("UpdateTransaction")]
    public async Task<ApiResponse<bool>> UpdateTransaction([FromBody] Transaction transaction)
    {
        return await transactionService.Update(transaction);
    }

    [HttpDelete("DeleteTransaction/{id}")]
    public async Task<ApiResponse<bool>> DeleteTransaction(int id)
    {
        return await transactionService.Delete(id);
    }

    [HttpGet("GetHistoryGet/{id}")]
    public async Task<ApiResponse<List<string>>> GetHistoryGet(int id)
    {
        return await transactionService.GetTransactionHistorySend(id);
    }

    [HttpGet("GetHistoryGetByDate/{id}")]
    public async Task<ApiResponse<List<string>>> GetHistoryRe(int id)
    {
        return await transactionService.GetTransactionHistoryReceive(id);
    }
}
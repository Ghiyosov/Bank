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
    public ApiResponse<List<Transaction>> GetTransactions()
    {
        return transactionService.GetAll();
    }

    [HttpGet("GetTransactions/{id}")]
    public ApiResponse<Transaction> GetTransaction(int id)
    {
        return transactionService.GetById(id);
    }

    [HttpPost("CreateTransaction")]
    public ApiResponse<bool> CreateTransaction([FromBody] Transaction transaction)
    {
        return transactionService.Add(transaction);
    }

    [HttpPut("UpdateTransaction")]
    public ApiResponse<bool> UpdateTransaction([FromBody] Transaction transaction)
    {
        return transactionService.Update(transaction);
    }

    [HttpDelete("DeleteTransaction/{id}")]
    public ApiResponse<bool> DeleteTransaction(int id)
    {
        return transactionService.Delete(id);
    }

    [HttpGet("GetHistoryGet/{id}")]
    public ApiResponse<List<string>> GetHistoryGet(int id)
    {
        return transactionService.GetTransactionHistorySend(id);
    }

    [HttpGet("GetHistoryGetByDate/{id}")]
    public ApiResponse<List<string>> GetHistoryRe(int id)
    {
        return transactionService.GetTransactionHistoryReceive(id);
    }
}
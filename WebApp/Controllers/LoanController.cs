using Domain.Entities;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class LoanController(IGenericService<Loan> loanService): ControllerBase
{
    [HttpGet("GetLoans")]
    public async Task<ApiResponse<List<Loan>>> GetLoans()
    {
        return await loanService.GetAll();
    }

    [HttpGet("GetLoan/{id}")]
    public async Task<ApiResponse<Loan>> GetLoan(int id)
    {
        return await loanService.GetById(id);
    }

    [HttpPost("AddLoan")]
    public async Task<ApiResponse<bool>> AddLoan(Loan loan)
    {
        return await loanService.Add(loan);
    }

    [HttpPost("UpdateLoan")]
    public async Task<ApiResponse<bool>> UpdateLoan(Loan loan)
    {
        return await loanService.Update(loan);
    }

    [HttpPost("DeleteLoan")]
    public async Task<ApiResponse<bool>> DeleteLoan(int id)
    {
        return await loanService.Delete(id);
    }
}
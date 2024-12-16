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
    public ApiResponse<List<Loan>> GetLoans()
    {
        return loanService.GetAll();
    }

    [HttpGet("GetLoan/{id}")]
    public ApiResponse<Loan> GetLoan(int id)
    {
        return loanService.GetById(id);
    }

    [HttpPost("AddLoan")]
    public ApiResponse<bool> AddLoan(Loan loan)
    {
        return loanService.Add(loan);
    }

    [HttpPost("UpdateLoan")]
    public ApiResponse<bool> UpdateLoan(Loan loan)
    {
        return loanService.Update(loan);
    }

    [HttpPost("DeleteLoan")]
    public ApiResponse<bool> DeleteLoan(int id)
    {
        return loanService.Delete(id);
    }
}
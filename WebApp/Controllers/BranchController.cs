using Domain.Entities;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class BranchController(IGenericService<Branch> branchService) : ControllerBase
{
    [HttpGet("GetBranches")]
    public ApiResponse<List<Branch>> GetBranches()
    {
        return branchService.GetAll();
    }

    [HttpGet("GetBranchById")]
    public ApiResponse<Branch> GetBranchById(int id)
    {
        return branchService.GetById(id);
    }

    [HttpPost("CreateBranch")]
    public ApiResponse<bool> CreateBranch(Branch branch)
    {
        return branchService.Add(branch);
    }

    [HttpPut("UpdateBranch")]
    public ApiResponse<bool> UpdateBranch(Branch branch)
    {
        return branchService.Update(branch);
    }

    [HttpDelete("DeleteBranch")]
    public ApiResponse<bool> DeleteBranch(int id)
    {
        return branchService.Delete(id);
    }
    
}
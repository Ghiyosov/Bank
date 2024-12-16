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
    public async Task<ApiResponse<List<Branch>>> GetBranches()
    {
        return await branchService.GetAll();
    }

    [HttpGet("GetBranchById")]
    public async Task<ApiResponse<Branch>> GetBranchById(int id)
    {
        return await branchService.GetById(id);
    }

    [HttpPost("CreateBranch")]
    public async Task<ApiResponse<bool>> CreateBranch(Branch branch)
    {
        return await branchService.Add(branch);
    }

    [HttpPut("UpdateBranch")]
    public async Task<ApiResponse<bool>> UpdateBranch(Branch branch)
    {
        return await branchService.Update(branch);
    }

    [HttpDelete("DeleteBranch")]
    public async Task<ApiResponse<bool>> DeleteBranch(int id)
    {
        return await branchService.Delete(id);
    }
    
}
using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Responses;
using Infratructure.Services;

namespace Infratructure.Service;

public class BranchService(IContext _context): IGenericService<Branch>
{
    public async Task<ApiResponse<List<Branch>>> GetAll()
    {
        var sql = @"select * from branches";
        var res = await _context.Connection().QueryAsync<Branch>(sql);
        return new ApiResponse<List<Branch>>(res.ToList());
    }

    public async Task<ApiResponse<Branch>> GetById(int id)
    {
        var sql = @"select * from branches where branchid = @id";
        var res = await _context.Connection().QuerySingleOrDefaultAsync<Branch>(sql, new { id });
        return new ApiResponse<Branch>(res);
    }

    public async Task<ApiResponse<bool>> Add(Branch data)
    {
        var sql = @"insert into branches (branchname,branchlocation,createdat) values (@branchname,@branchlocation,@currentdate)";
        var res = await _context.Connection().ExecuteAsync(sql, data);
        return res == 1 ? new ApiResponse<bool>(HttpStatusCode.Created, "Branch is created sucsesifull") : new ApiResponse<bool>(false);
    }

    public async Task<ApiResponse<bool>> Update(Branch data)
    {
        var sql = @"update branches set branchname = @branchname, branchlocation = @branchlocation, createdat=@createdat where branchid = @branchid";
        var res = await _context.Connection().ExecuteAsync(sql, data);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Branch is updated sucsesifull");


    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        var sql = @"delete from branches where branchid = @branchid";
        var res = await _context.Connection().ExecuteAsync(sql, new { branchid = id });
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<bool>(HttpStatusCode.OK, "Branch is deleted sucsesifull");
    }
}
using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Responses;
using Infratructure.Services;

namespace Infratructure.Service;

public class BranchService(IContext _context): IGenericService<Branch>
{
    public ApiResponse<List<Branch>> GetAll()
    {
        var sql = @"select * from branches";
        var res = _context.Connection().Query<Branch>(sql).ToList();
        return new ApiResponse<List<Branch>>(res);
    }

    public ApiResponse<Branch> GetById(int id)
    {
        var sql = @"select * from branches where branchid = @id";
        var res = _context.Connection().QuerySingleOrDefault<Branch>(sql, new { id });
        return new ApiResponse<Branch>(res);
    }

    public ApiResponse<bool> Add(Branch data)
    {
        var sql = @"insert into branches (branchname,branchlocation,createdat) values (@branchname,@branchlocation,@currentdate)";
        var res = _context.Connection().Execute(sql, data);
        return res == 1 ? new ApiResponse<bool>(HttpStatusCode.Created, "Branch is created sucsesifull") : new ApiResponse<bool>(false);
    }

    public ApiResponse<bool> Update(Branch data)
    {
        var sql = @"update branches set branchname = @branchname, branchlocation = @branchlocation, createdat=@createdat where branchid = @branchid";
        var res = _context.Connection().Execute(sql, data);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Branch is updated sucsesifull");


    }

    public ApiResponse<bool> Delete(int id)
    {
        var sql = @"delete from branches where branchid = @branchid";
        var res = _context.Connection().Execute(sql, new { branchid = id });
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<bool>(HttpStatusCode.OK, "Branch is deleted sucsesifull");
    }
}
using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Responses;

namespace Infratructure.Services;

public class LoanService(IContext _context): IGenericService<Loan>
{
    public async Task<ApiResponse<List<Loan>>> GetAll()
    {
        var sql = @"select * from Loans";
        var res = await _context.Connection().QueryAsync<Loan>(sql);
        return new ApiResponse<List<Loan>>(res.ToList());
    }

    public async Task<ApiResponse<Loan>> GetById(int id)
    {
        var sql = @"select * from Loans where LoanId = @id";
        var res = _context.Connection().QuerySingleOrDefault<Loan>(sql, new { id });
        return new ApiResponse<Loan>(res);
    }

    public async Task<ApiResponse<bool>> Add(Loan data)
    {
        var sql =
            "insert into Loans(loanamount,dateissued,createdat,deletedat,customerid,branchid) values(@loanamount, @dateissued, @createdat, @deletedat, @customerid, @branchid)";
        var res = await _context.Connection().ExecuteAsync(sql, data);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Branch is created successful");
    }

    public async Task<ApiResponse<bool>> Update(Loan data)
    {
        var sql = @"update Loans set loanamount = @loanamount, dateissued = @dateissued, createdat=@createdat, deletedat=@deletedat, customerid=@customerid, branchid=@branchid  where loanId = @loanId";
        var res = await _context.Connection().ExecuteAsync(sql, data);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Loan is updated successful");
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        var sql = @"delete from Loans where LoanId = @id";
        var res = await _context.Connection().ExecuteAsync(sql, new { id });
        return res == 0 
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Loan is deleted successful");
    }
}
using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Responses;

namespace Infratructure.Services;

public class AccountService(IContext _context): IGenericService<Account>
{
    public async Task<ApiResponse<List<Account>>> GetAll()
    {
        var sql = @"select * from Accounts";
        var res = await _context.Connection().QueryAsync<Account>(sql);
        return new ApiResponse<List<Account>>(res.ToList());
    }

    public async Task<ApiResponse<Account>> GetById(int id)
    {
        var sql = @"select * from Accounts where AccountId = @id";
        var res = await _context.Connection().QuerySingleOrDefaultAsync<Account>(sql, new { id });
        return new ApiResponse<Account>(res);
    }

    public async Task<ApiResponse<bool>> Add(Account data)
    {
        var sql = "insert into Accounts (Balance, AccountStatus, AccountType, Currency,Customerid, CreatedAt, DeletedAt) values (@Balance, @AccountStatus, @AccountType, @Currency, @Customerid, @CreatedAt, @DeletedAt)";
        var res = await _context.Connection().ExecuteAsync(sql, data);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error ")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Account successfully added");
    }

    public async Task<ApiResponse<bool>> Update(Account data)
    {
        var sql = "update Accounts set Balance=@Balance, AccountStatus=@AccountStatus, AccountType=@AccountType, Currency=@Currency, Customerid=@Customerid, CreatedAt=@CreatedAt, DeletedAt=@DeletedAt where AccountId = @AccountId";
        var res = await _context.Connection().ExecuteAsync(sql, data);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error ")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Account successfully updated");
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        var sql = @"delete from Accounts where AccountId = @id";
        var res = await _context.Connection().ExecuteAsync(sql, new { id });
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error ")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Account successfully deleted");
    }

    public async Task<ApiResponse<bool>> ReplenishBalance(int accountId,decimal amount) //пополнение баланса
    {
        var sql = "update Accounts set Balance=Balance+@amount where AccountId = @accountId returning Balance";
        var res = await _context.Connection().ExecuteScalarAsync<decimal>(sql, new { accountId , amount });
        return res == null
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error ")
            : new ApiResponse<bool>(HttpStatusCode.Created, $"Balance successfully replenished {res}");
    }

    public async Task<ApiResponse<bool>> WithdrawnMony(int accountId, decimal amount)//снятие денег
    {
        var sqlBalance = "select Balance from Accounts where AccountId = @accountId";
        var resBalance =  _context.Connection().ExecuteScalar<decimal>(sqlBalance, new { accountId, amount });
        if (resBalance >= amount)
        {
            var sql = "update Accounts set Balance=Balance-@amount where AccountId = @accountId returning Balance";
            var res = await _context.Connection().ExecuteScalarAsync<decimal>(sql, new { accountId, amount });
            return new ApiResponse<bool>(HttpStatusCode.OK, @$"Mony successfully Withdrawn. Balance on account : {res}");
        }

        return new ApiResponse<bool>(HttpStatusCode.NotAcceptable, $"Insufficient funds in the account {resBalance}");
    }
}
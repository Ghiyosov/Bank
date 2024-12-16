using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Responses;

namespace Infratructure.Services;

public class AccountService(IContext _context): IGenericService<Account>
{
    public ApiResponse<List<Account>> GetAll()
    {
        var sql = @"select * from Accounts";
        var res = _context.Connection().Query<Account>(sql).ToList();
        return new ApiResponse<List<Account>>(res);
    }

    public ApiResponse<Account> GetById(int id)
    {
        var sql = @"select * from Accounts where AccountId = @id";
        var res = _context.Connection().QuerySingleOrDefault<Account>(sql, new { id });
        return new ApiResponse<Account>(res);
    }

    public ApiResponse<bool> Add(Account data)
    {
        var sql = "insert into Accounts (Balance, AccountStatus, AccountType, Currency,Customerid, CreatedAt, DeletedAt) values (@Balance, @AccountStatus, @AccountType, @Currency, @Customerid, @CreatedAt, @DeletedAt)";
        var res = _context.Connection().Execute(sql, data);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error ")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Account successfully added");
    }

    public ApiResponse<bool> Update(Account data)
    {
        var sql = "update Accounts set Balance=@Balance, AccountStatus=@AccountStatus, AccountType=@AccountType, Currency=@Currency, Customerid=@Customerid, CreatedAt=@CreatedAt, DeletedAt=@DeletedAt where AccountId = @AccountId";
        var res = _context.Connection().Execute(sql, data);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error ")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Account successfully updated");
    }

    public ApiResponse<bool> Delete(int id)
    {
        var sql = @"delete from Accounts where AccountId = @id";
        var res = _context.Connection().Execute(sql, new { id });
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error ")
            : new ApiResponse<bool>(HttpStatusCode.Created, "Account successfully deleted");
    }

    public ApiResponse<bool> ReplenishBalance(int accountId,decimal amount) //пополнение баланса
    {
        var sql = "update Accounts set Balance=Balance+@amount where AccountId = @accountId returning Balance";
        var res = _context.Connection().ExecuteScalar<decimal>(sql, new { accountId , amount });
        return res == null
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal Server Error ")
            : new ApiResponse<bool>(HttpStatusCode.Created, $"Balance successfully replenished {res}");
    }

    public ApiResponse<bool> WithdrawnMony(int accountId, decimal amount)//снятие денег
    {
        var sqlBalance = "select Balance from Accounts where AccountId = @accountId";
        var resBalance = _context.Connection().ExecuteScalar<decimal>(sqlBalance, new { accountId, amount });
        if (resBalance >= amount)
        {
            var sql = "update Accounts set Balance=Balance-@amount where AccountId = @accountId returning Balance";
            var res = _context.Connection().ExecuteScalar<decimal>(sql, new { accountId, amount });
            return new ApiResponse<bool>(HttpStatusCode.OK, @$"Mony successfully Withdrawn. Balance on account : {res}");
        }

        return new ApiResponse<bool>(HttpStatusCode.NotAcceptable, $"Insufficient funds in the account {resBalance}");
    }
}
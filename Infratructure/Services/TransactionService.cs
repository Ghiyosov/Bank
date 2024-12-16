using System.Net;
using Domain.Entities;
using Dapper;
using Infratructure.DataContext;
using Infratructure.Responses;

namespace Infratructure.Services;

public class TransactionService(IContext _context): IGenericService<Transaction>
{
    public ApiResponse<List<Transaction>> GetAll()
    {
        var sql = @"select * from Transactions";
        var res = _context.Connection().Query<Transaction>(sql).ToList();
        return new ApiResponse<List<Transaction>>(res);
    }

    public ApiResponse<Transaction> GetById(int id)
    {
        var sql = @"select * from Transactions where TransactionId = @id";
        var res = _context.Connection().QuerySingleOrDefault<Transaction>(sql, new { id });
        return new ApiResponse<Transaction>(res);
    }

    public ApiResponse<bool> Add(Transaction data)
    {
       var sqlFromAccount = @"select balance from Accounts where AccountId = @FromAccountId"; 
       var resFromAccount = _context.Connection().QuerySingleOrDefault<decimal>(sqlFromAccount, new { data.FromAccountId });
       if (resFromAccount>=data.Amount)
       {
           var sql ="insert into Transactions (transactionstatus,dateissued, Amount,createdat,deletedat,fromaccountid,toaccountid) values (@transactionstatus,@dateissued,@Amount,@createdat,@deletedat,@fromaccountid,@toaccountid)";
           var res = _context.Connection().Execute(sql, data);
           AccountService accountService = new AccountService(_context);
           accountService.ReplenishBalance(data.FromAccountId, data.Amount);
           accountService.WithdrawnMony(data.ToAccountId, data.Amount);
           return res == 0
               ? new ApiResponse<bool>(HttpStatusCode.InternalServerError,"Internal Server Error")
               : new ApiResponse<bool>(HttpStatusCode.OK, "Transaction added successfully");
       }
       return new ApiResponse<bool>(HttpStatusCode.NotAcceptable,"Transaction does not exist ");
    }

    public ApiResponse<bool> Update(Transaction data)
    {
        TransactionService transactionService = new TransactionService(_context);
        var transaction = transactionService.GetById(data.TransactionId);
        AccountService accountService = new AccountService(_context);
        accountService.WithdrawnMony(transaction.Data.FromAccountId, transaction.Data.Amount);
        accountService.ReplenishBalance(transaction.Data.ToAccountId, transaction.Data.Amount);
        
        var sql = "update Transactions set transactionstatus=@transactionstatus,dateissued=@dateissued,Amount=@Amount,createdat=@createdat,deletedat=@deletedat,fromaccountid=@fromaccountid,toaccountid=@toaccountid where TransactionId = @TransactionId";
        var res = _context.Connection().Execute(sql, data);
        accountService.ReplenishBalance(data.FromAccountId, data.Amount);
        accountService.WithdrawnMony(data.ToAccountId, data.Amount);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError,"Internal Server Error")
            : new ApiResponse<bool>(HttpStatusCode.OK, "Transaction updated successfully");
    }

    public ApiResponse<bool> Delete(int id)
    {
        TransactionService transactionService = new TransactionService(_context);
        var transaction = transactionService.GetById(id);
        AccountService accountService = new AccountService(_context);
        accountService.WithdrawnMony(transaction.Data.FromAccountId, transaction.Data.Amount);
        accountService.ReplenishBalance(transaction.Data.ToAccountId, transaction.Data.Amount);
        var sql = "delete from Transactions where TransactionId = @id";
        var res = _context.Connection().Execute(sql, transaction);
        return res == 0
            ? new ApiResponse<bool>(HttpStatusCode.InternalServerError,"Internal Server Error")
            : new ApiResponse<bool>(HttpStatusCode.OK, "Transaction deleted successfully");
    }

    public ApiResponse<List<string>> GetTransactionHistorySend(int id)
    {
        var sql = @"select 'From '||a.AccountId||' to '||t.ToAccountId||' - '||Amount 
                   from Transactions as t
                    join Accounts as a on t.FromAccountId= @id";
        var res = _context.Connection().Query<string>(sql,new {id}).ToList();
        return new ApiResponse<List<string>>(res);
    }
    
    public ApiResponse<List<string>> GetTransactionHistoryReceive(int id)
    {
        var sql = @"select 'From '||t.ToAccountId||' to '||a.AccountId||' - '||Amount 
                   from Transactions as t
                    join Accounts as a on t.FromAccountId= @id";
        var res = _context.Connection().Query<string>(sql,new {id}).ToList();
        return new ApiResponse<List<string>>(res);
    }
}
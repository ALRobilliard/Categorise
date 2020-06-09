using System;
using System.Collections.Generic;
using System.Linq;
using CategoriseApi.Helpers;
using CategoriseApi.Models;

namespace CategoriseApi.Services
{
  public interface ITransactionService
  {
    IEnumerable<Transaction> GetTransactions();
    Transaction GetTransaction(Guid id);
    Transaction CreateTransaction(Transaction transaction);
    void UpdateTransaction(Transaction transaction);
    void DeleteTransaction(Guid id);
  }

  public class TransactionService : ITransactionService
  {
    private CategoriseContext _context;

    public TransactionService(CategoriseContext context)
    {
      _context = context;
    }

    public IEnumerable<Transaction> GetTransactions()
    {
      return _context.Transactions;
    }

    public Transaction GetTransaction(Guid id)
    {
      return _context.Transactions.Find(id);
    }

    public Transaction CreateTransaction(Transaction transaction)
    {
      _context.Transactions.Add(transaction);
      _context.SaveChanges();
      
      return transaction;
    }

    public void UpdateTransaction(Transaction transactionParam)
    {
      Transaction transaction = _context.Transactions.Find(transactionParam.Id);

      // Update transaction properties.
      transaction.Account = transactionParam.Account;
      transaction.Amount = transactionParam.Amount;
      transaction.Category = transactionParam.Category;
      transaction.IsShared = transactionParam.IsShared;

      _context.Transactions.Update(transaction);
      _context.SaveChanges();
    }

    public void DeleteTransaction(Guid id)
    {
      var transaction = _context.Transactions.Find(id);
      if (transaction != null)
      {
        _context.Transactions.Remove(transaction);
        _context.SaveChanges();
      }
    }
  }
}
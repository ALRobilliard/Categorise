using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CategoriseApi.Helpers;
using CategoriseApi.Models;
using CategoriseApi.Services;

namespace CategoriseApi.Services
{
  /// <summary>
  /// Service for exposing upload actions for Transaction.
  /// </summary>
  public interface ITransactionUploadService
  {
    /// <summary>
    /// Creates bulk transactions from CSV input.
    /// </summary>
    void UploadCsv(string b64Content, Guid userId);
  }

  /// <summary>
  /// Service for exposing upload actions for Transaction.
  /// </summary>
  public class TransactionUploadService : ITransactionUploadService
  {
    private CategoriseContext _context;
    private TransactionService _transactionService;
    private AccountService _accountService;

    /// <summary>
    /// Constructor for TransactionUploadService.
    /// </summary>
    public TransactionUploadService(CategoriseContext context)
    {
      _context = context;
      _transactionService = new TransactionService(context);
      _accountService = new AccountService(context);
    }

    /// <summary>
    /// Creates bulk transactions from CSV input.
    /// </summary>   
    public void UploadCsv(string b64Content, Guid userId)
    {
      string csvContent = GetCsvContent(b64Content);

      string accountRow = csvContent.Split('\n')[1];
      Account account = GetAccount(accountRow, userId);

      List<string> exportedRows = csvContent.Split('\n').Skip(6).ToList();
      exportedRows = exportedRows.Where(r => !string.IsNullOrEmpty(r)).ToList();

      ProcessRows(exportedRows, account.Id, userId);
    }

    private string GetCsvContent(string b64Content)
    {
      byte[] data = Convert.FromBase64String(b64Content);
      string decodedString = Encoding.UTF8.GetString(data);
      return decodedString;
    }

    private Account GetAccount(string accountRow, Guid userId)
    {
      int startIdx = accountRow.IndexOf('(');
      int endIdx = accountRow.IndexOf(')');
      string accountName = accountRow.Substring(startIdx + 1, endIdx - startIdx - 1);

      Account account = _accountService.GetAccountByName(accountName, userId);

      if (account == null)
      {
        account = _accountService.CreateAccount(accountName, userId);
      }

      return account;
    }

    private void ProcessRows(List<string> transactionRows, Guid accountId, Guid userId)
    {
      foreach(var row in transactionRows)
      {
        string[] rowAttributes = row.Split(',');
        string[] transactionDate = rowAttributes[1].Replace("\r", "").Split('/');
        Transaction transaction = new Transaction
        {
          TransactionDate = new DateTime(int.Parse(transactionDate[0]), int.Parse(transactionDate[1]), int.Parse(transactionDate[2])),
          TransactionType = rowAttributes[3].Replace("\r", ""),
          Amount = decimal.Parse(rowAttributes[6]),
          AccountId = accountId,
          UserId = userId
        };

        _transactionService.CreateTransaction(transaction);
      }
    }
  }
}
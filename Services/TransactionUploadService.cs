using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CategoriseApi.Helpers;
using CategoriseApi.Models;
using CategoriseApi.Services;

namespace CategoriseApi.Services
{
  public interface ITransactionUploadService
  {
    void UploadCsv(string b64Content);
  }

  public class TransactionUploadService : ITransactionUploadService
  {
    private CategoriseContext _context;
    private TransactionService _transactionService;
    private AccountService _accountService;

    public TransactionUploadService(CategoriseContext context)
    {
      _context = context;
      _transactionService = new TransactionService(context);
      _accountService = new AccountService(context);
    }

    public void UploadCsv(string b64Content)
    {
      string csvContent = GetCsvContent(b64Content);

      string accountRow = csvContent.Split('\n')[1];
      Account account = GetAccount(accountRow);

      List<string> exportedRows = csvContent.Split('\n').Skip(6).ToList();
      exportedRows = exportedRows.Where(r => !string.IsNullOrEmpty(r)).ToList();

      ProcessRows(exportedRows, account);
    }

    private string GetCsvContent(string b64Content)
    {
      byte[] data = Convert.FromBase64String(b64Content);
      string decodedString = Encoding.UTF8.GetString(data);
      return decodedString;
    }

    private Account GetAccount(string accountRow)
    {
      int startIdx = accountRow.IndexOf('(');
      int endIdx = accountRow.IndexOf(')');
      string accountName = accountRow.Substring(startIdx + 1, endIdx - startIdx - 1);

      Account account = _accountService.GetAccountByName(accountName);

      if (account == null)
      {
        account = _accountService.CreateAccount(accountName, null);
      }

      return account;
    }

    private void ProcessRows(List<string> transactionRows, Account account)
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
          Account = account
        };

        _transactionService.CreateTransaction(transaction);
      }
    }
  }
}
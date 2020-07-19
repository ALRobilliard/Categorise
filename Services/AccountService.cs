using System;
using System.Collections.Generic;
using System.Linq;
using CategoriseApi.Models;

namespace CategoriseApi.Services
{
  public interface IAccountService
  {
    IEnumerable<Account> GetAccounts(Guid userId);
    Account GetAccountById(Guid id, Guid userId);
    Account GetAccountByName(string name, Guid userId);
    Account CreateAccount(string accountName, Guid userId);
  }

  public class AccountService : IAccountService
  {
    private CategoriseContext _context;

    public AccountService(CategoriseContext context)
    {
      _context = context;
    }

    public IEnumerable<Account> GetAccounts(Guid userId)
    {
      return _context.Accounts.Where(a => a.UserId == userId);
    }

    public Account GetAccountById(Guid id, Guid userId)
    {
      return _context.Accounts.Where(a => a.Id == id && a.UserId == userId).FirstOrDefault();
    }

    public Account GetAccountByName(string name, Guid userId)
    {
      return _context.Accounts
        .Where(a => a.AccountName == name && a.UserId == userId).FirstOrDefault();
    }

    public Account CreateAccount(string accountName, Guid userId)
    {
      Account account = new Account
      {
        AccountName = accountName,
        UserId = userId
      };

      _context.Accounts.Add(account);
      _context.SaveChanges();
      
      return account;
    }
  }
}
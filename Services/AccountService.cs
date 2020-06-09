using System;
using System.Collections.Generic;
using System.Linq;
using CategoriseApi.Models;

namespace CategoriseApi.Services
{
  public interface IAccountService
  {
    IEnumerable<Account> GetAccounts();
    Account GetAccountById(Guid id);
    Account GetAccountByName(string name);
    Account CreateAccount(string accountName, User user);
  }

  public class AccountService : IAccountService
  {
    private CategoriseContext _context;

    public AccountService(CategoriseContext context)
    {
      _context = context;
    }

    public IEnumerable<Account> GetAccounts()
    {
      return _context.Accounts;
    }

    public Account GetAccountById(Guid id)
    {
      return _context.Accounts.Find(id);
    }

    public Account GetAccountByName(string name)
    {
      return _context.Accounts.Where(a => a.AccountName == name).FirstOrDefault();
    }

    public Account CreateAccount(string accountName, User user)
    {
      Account account = new Account
      {
        AccountName = accountName,
        User = user
      };

      _context.Accounts.Add(account);
      _context.SaveChanges();
      
      return account;
    }
  }
}
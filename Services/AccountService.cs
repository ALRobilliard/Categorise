using System;
using System.Collections.Generic;
using System.Linq;
using CategoriseApi.Models;

namespace CategoriseApi.Services
{
  /// <summary>
  /// Service for exposing common actions for Account.
  /// </summary>
  public interface IAccountService
  {
    /// <summary>
    /// Retrieve all accounts for the specified user.
    /// </summary>
    IEnumerable<Account> GetAccounts(Guid userId);

    /// <summary>
    /// Retrieve a single account for the specified user by account unique identifier.
    /// </summary>
    Account GetAccountById(Guid id, Guid userId);

    /// <summary>
    /// Retrieve a single account for the specified user by account name.
    /// </summary>
    Account GetAccountByName(string name, Guid userId);

    /// <summary>
    /// Creates an account for the specified user.
    /// </summary>
    Account CreateAccount(string accountName, Guid userId);
  }

  /// <summary>
  /// Service for exposing common actions for Account.
  /// </summary>
  public class AccountService : IAccountService
  {
    private CategoriseContext _context;

    /// <summary>
    /// Constructor for the AccountService.
    /// </summary>
    public AccountService(CategoriseContext context)
    {
      _context = context;
    }

    /// <summary>
    /// Retrieve all accounts for the specified user.
    /// </summary>
    public IEnumerable<Account> GetAccounts(Guid userId)
    {
      return _context.Accounts.Where(a => a.UserId == userId);
    }

    /// <summary>
    /// Retrieve a single account for the specified user by account unique identifier.
    /// </summary>
    public Account GetAccountById(Guid id, Guid userId)
    {
      return _context.Accounts.Where(a => a.Id == id && a.UserId == userId).FirstOrDefault();
    }

    /// <summary>
    /// Retrieve a single account for the specified user by account name.
    /// </summary>
    public Account GetAccountByName(string name, Guid userId)
    {
      return _context.Accounts
        .Where(a => a.AccountName == name && a.UserId == userId).FirstOrDefault();
    }

    /// <summary>
    /// Creates an account for the specified user.
    /// </summary>
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
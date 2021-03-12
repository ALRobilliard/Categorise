using System;
using System.Collections.Generic;
using System.Linq;
using Categorise.Data;

namespace Categorise.Services
{
    /// <summary>
    /// Service for exposing common actions for Account.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Retrieve all accounts for the specified user.
        /// </summary>
        IEnumerable<Account> GetAccounts(string userId);

        /// <summary>
        /// Retrieve a single account for the specified user by account unique identifier.
        /// </summary>
        Account GetAccountById(Guid id, string userId);

        /// <summary>
        /// Retrieve a single account for the specified user by account name.
        /// </summary>
        Account GetAccountByName(string name, string userId);

        /// <summary>
        /// Creates an account for the specified user.
        /// </summary>
        Account CreateAccount(Account accountDto, string userId);

        /// <summary>
        /// Updates an account for the specified user.
        /// </summary>
        void UpdateAccount(Account account, string userId);

        /// <summary>
        /// Deletes the specified account for the specified user.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userId"></param>
        void DeleteAccount(Guid accountId, string userId);

        /// <summary>
        /// Search for the an account by account name.
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Account> SearchAccounts(string accountName, string userId);
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
        public IEnumerable<Account> GetAccounts(string userId)
        {
            return _context.Accounts.Where(a => a.UserId == userId);
        }

        /// <summary>
        /// Retrieve a single account for the specified user by account unique identifier.
        /// </summary>
        public Account GetAccountById(Guid id, string userId)
        {
            return _context.Accounts.Where(a => a.Id == id && a.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// Retrieve a single account for the specified user by account name.
        /// </summary>
        public Account GetAccountByName(string name, string userId)
        {
            return _context.Accounts
              .Where(a => a.AccountName == name && a.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// Creates an account for the specified user.
        /// </summary>
        public Account CreateAccount(Account accountDto, string userId)
        {
            Account account = new Account
            {
                AccountName = accountDto.AccountName,
                AccountType = accountDto.AccountType,
                Balance = accountDto.Balance,
                CreditLimit = accountDto.CreditLimit,
                UserId = userId
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account;
        }

        /// <summary>
        /// Updates an account for the specified user.
        /// </summary>
        public void UpdateAccount(Account accountDto, string userId)
        {
            Account account = _context.Accounts.Find(accountDto.Id);

            if (account != null)
            {
                account.AccountName = accountDto.AccountName;
                account.AccountType = accountDto.AccountType;
                account.Balance = accountDto.Balance;
                account.CreditLimit = accountDto.CreditLimit;

                _context.Accounts.Update(account);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified account for the specified user.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userId"></param>
        public void DeleteAccount(Guid accountId, string userId)
        {
            Account account = _context.Accounts.Find(accountId);

            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Account> SearchAccounts(string accountName, string userId)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                return new List<Account>();
            }

            return _context.Accounts
              .Where(
                  a => a.AccountName.ToLower().StartsWith(accountName.ToLower()) &&
                  a.UserId == userId
                ).ToList();
        }
    }
}
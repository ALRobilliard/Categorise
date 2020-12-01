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
        Account CreateAccount(Account account, string userId);

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
        public Account CreateAccount(Account account, string userId)
        {
            account.UserId = userId;
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account;
        }

        /// <summary>
        /// Updates an account for the specified user.
        /// </summary>
        public void UpdateAccount(Account accountInput, string userId)
        {
            Account account = _context.Accounts.Find(accountInput.Id);

            if (account != null)
            {
                account.AccountName = accountInput.AccountName;
                account.Balance = accountInput.Balance;
                account.AccountType = accountInput.AccountType;
                account.CreditLimit = accountInput.CreditLimit;

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
    }
}
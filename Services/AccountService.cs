using System;
using System.Collections.Generic;
using System.Linq;
using Categorise.Models;
using Categorise.Dtos;

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
        Account CreateAccount(AccountDto accountDto, Guid userId);

        /// <summary>
        /// Updates an account for the specified user.
        /// </summary>
        void UpdateAccount(AccountDto accountDto, Guid userId);

        /// <summary>
        /// Deletes the specified account for the specified user.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userId"></param>
        void DeleteAccount(Guid accountId, Guid userId);
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
        public Account CreateAccount(AccountDto accountDto, Guid userId)
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
        public void UpdateAccount(AccountDto accountDto, Guid userId)
        {
            Account account = _context.Accounts.Find(accountDto.Id);

            if (account != null)
            {
                account.AccountName = accountDto.AccountName;
                account.Balance = accountDto.Balance;
                account.AccountType = accountDto.AccountType;
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
        public void DeleteAccount(Guid accountId, Guid userId)
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Categorise.Data
{
    /// <summary>
    /// Account Entity Model
    /// </summary>
    public class Account : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Account name.
        /// </summary>
        [Required]
        [MaxLength(25)]
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the account balance.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        public int AccountType { get; set; }

        /// <summary>
        /// Gets or sets the credit limit.
        /// </summary>
        public decimal? CreditLimit { get; set; }

        /// <summary>
        /// Unique identifier for the owning user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Owning user.
        /// </summary>
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        /// <summary>
        /// List of transactions associated with this account.
        /// </summary>
        public List<Transaction> Transactions { get; set; }
    }

    /// <summary>
    /// Account type enum.
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Credit account type.
        /// </summary>
        Credit,

        /// <summary>
        /// Debit account type.
        /// </summary>
        Debit
    }
}
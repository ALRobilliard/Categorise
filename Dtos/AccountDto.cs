using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriseApi.Dtos
{
    /// <summary>
    /// Data transfer object for Account.
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// Gets or sets the account ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the account name.
        /// </summary>
        [Required]
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the account balance.
        /// </summary>
        [Required]
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        [Required]
        public int AccountType { get; set; }

        /// <summary>
        /// Gets or sets the credit limit.
        /// </summary>
        public decimal? CreditLimit { get; set; }
    }
}
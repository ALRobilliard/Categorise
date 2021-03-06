using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Categorise.Data
{
    /// <summary>
    /// Transaction entity model.
    /// </summary>
    public class Transaction : BaseEntity
    {
        /// <summary>
        /// Gets or sets the transaction type.
        /// </summary>
        [Required]
        [MaxLength(25)]
        public string TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the 'is shared' flag.
        /// </summary>
        public bool IsShared { get; set; }

        /// <summary>
        /// Gets or sets the transaction date.
        /// </summary>
        [Required]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Unqiue identifier for the associated account.
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Associated account.
        /// </summary>
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        /// <summary>
        /// Unique identifier for the associated category.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Associated category.
        /// </summary>
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        /// <summary>
        /// Unique identifier for the associated Vendor.
        /// </summary>
        /// <value></value>
        public Guid VendorId { get; set; }

        /// <summary>
        /// Associated vendor.
        /// </summary>
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

        /// <summary>
        /// Unqique identifier for the owning user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Owning user.
        /// </summary>
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        /// <summary>
        /// List of transaction notes associated with this transaction.
        /// </summary>
        public List<TransactionNote> TransactionNotes { get; set; }

        /// <summary>
        /// List of transaction tags associated with this transaction.
        /// </summary>
        public List<TransactionTag> TransactionTags { get; set; }
    }
}
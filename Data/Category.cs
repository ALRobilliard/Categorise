using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Categorise.Data
{
    /// <summary>
    /// Category entity model.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Category name.
        /// </summary>
        [Required]
        [MaxLength(25)]
        public string CategoryName { get; set; }

        public bool IsGlobal { get; set; }

        /// <summary>
        /// Unqiue identifier for the owning user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Owning user.
        /// </summary>
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        /// <summary>
        /// List of transactions associated with this category.
        /// </summary>
        public List<Transaction> Transactions { get; set; }

        public List<Vendor> Vendors { get; set; }
    }
}
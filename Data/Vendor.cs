using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Categorise.Data
{
    /// <summary>
    /// Vendor entity model.
    /// </summary>
    public class Vendor : BaseEntity
    {
        /// <summary>
        /// Gets or sets the vendor name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string VendorName { get; set; }

        /// <summary>
        /// Unqiue identifier of the associated transaction.
        /// </summary>
        public Guid VendorId { get; set; }

        /// <summary>
        /// Associated transaction.
        /// </summary>
        [ForeignKey("TransactionForeignKey")]
        public Transaction Transaction { get; set; }

        /// <summary>
        /// Unique identifier of the owning user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Owning user.
        /// </summary>
        [ForeignKey("UserForeignKey")]
        public IdentityUser User { get; set; }
    }
}
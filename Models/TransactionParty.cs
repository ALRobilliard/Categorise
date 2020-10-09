using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Categorise.Models
{
    /// <summary>
    /// Transaction party entity model.
    /// </summary>
    public class TransactionParty : BaseEntity
    {
        /// <summary>
        /// Gets or sets the transaction party name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string TransactionPartyName { get; set; }

        /// <summary>
        /// Unqiue identifier of the associated transaction.
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Associated transaction.
        /// </summary>
        [ForeignKey("TransactionForeignKey")]
        public Transaction Transaction { get; set; }

        /// <summary>
        /// Unique identifier of the owning user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Owning user.
        /// </summary>
        [ForeignKey("UserForeignKey")]
        public User User { get; set; }
    }
}
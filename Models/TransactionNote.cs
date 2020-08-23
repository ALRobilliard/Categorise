using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
  /// <summary>
  /// Transaction note entity model.
  /// </summary>
  public class TransactionNote : BaseEntity
  {
    /// <summary>
    /// Gets or sets the transaction note subject.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string TransactionNoteSubject { get; set; }

    /// <summary>
    /// Gets or sets the transaction note body.
    /// </summary>
    [MaxLength(500)]
    public string TransactionNoteBody { get; set; }

    /// <summary>
    /// Unique identifier of the associated transaction.
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
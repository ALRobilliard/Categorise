using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
  /// <summary>
  /// Account entity model.
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
    /// Unique identifier for the owning user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Owning user.
    /// </summary>
    [ForeignKey("UserForeignKey")]
    public User User { get; set; }

    /// <summary>
    /// List of transactions associated with this account.
    /// </summary>
    public List<Transaction> Transactions { get; set; }
  }
}
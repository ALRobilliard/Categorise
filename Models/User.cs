using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CategoriseApi.Models
{
  /// <summary>
  /// User entity model.
  /// </summary>
  public class User : BaseEntity
  {
    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    [Required]
    [MaxLength(25)]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the 'is registered' flag.
    /// </summary>
    public bool? IsRegistered { get; set; }

    /// <summary>
    /// Gets or sets the 'confirmed email' flag.
    /// </summary>
    public bool? ConfirmedEmail { get; set; }

    /// <summary>
    /// Gets or sets the hash of the user's password.
    /// </summary>
    public byte[] PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the salt used in the user's password.
    /// </summary>
    public byte[] PasswordSalt { get; set; }

    /// <summary>
    /// Gets or sets the user's last login timestamp.
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// List of owned accounts.
    /// </summary>
    public List<Account> Accounts { get; set; }

    /// <summary>
    /// List of owned categories.
    /// </summary>
    public List<Category> Categories { get; set; }

    /// <summary>
    /// List of owned transactions.
    /// </summary>
    public List<Transaction> Transactions { get; set; }

    /// <summary>
    /// List of owned transaction notes.
    /// </summary>
    public List<TransactionNote> TransactionNotes { get; set; }

    /// <summary>
    /// List of owned transaction parties.
    /// </summary>
    public List<TransactionParty> TransactionParties { get; set; }

    /// <summary>
    /// List of owned transaction tags.
    /// </summary>
    public List<TransactionTag> TransactionTags { get; set; }
  }
}
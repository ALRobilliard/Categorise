using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CategoriseApi.Models
{
  public class User
  {
    [Key]
    public Guid UserId { get; set; }
    [Required]
    [MaxLength(25)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    public bool? IsRegistered { get; set; }
    public bool? ConfirmedEmail { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedOn { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime ModifiedOn { get; set; }
    public DateTime? LastLogin { get; set; }

    public List<Account> Accounts { get; set; }
    public List<Category> Categories { get; set; }
    public List<Transaction> Transactions { get; set; }
    public List<TransactionNote> TransactionNotes { get; set; }
    public List<TransactionParty> TransactionParties { get; set; }
    public List<TransactionTag> TransactionTags { get; set; }
  }
}
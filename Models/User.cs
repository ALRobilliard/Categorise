using System;
using System.Collections.Generic;

namespace CategoriseApi.Models
{
  public class User
  {
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool? IsRegistered { get; set; }
    public bool? ConfirmedEmail { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime CreatedOn { get; set; }
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
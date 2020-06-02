using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
    public class Transaction
    {
      public Guid TransactionId { get; set; }
      public string TransactionType { get; set; }
      public decimal Amount { get; set; }
      public bool IsShared { get; set; }
      public DateTime CreatedOn {get; set; }
      public DateTime ModifiedOn { get; set; }

      public Guid AccountId { get; set; }
      [ForeignKey("AccountForeignKey")]
      public Account Account { get; set; }
      public Guid CategoryId { get; set; }
      [ForeignKey("CategoryForeignKey")]
      public Category Category { get; set; }
      public Guid UserId { get; set; }
      [ForeignKey("UserForeignKey")]
      public User User { get; set; }

      public List<TransactionNote> TransactionNotes { get; set; }
      public List<TransactionParty> TransactionParties { get; set; }
      public List<TransactionTag> TransactionTags { get; set; }
    }
}
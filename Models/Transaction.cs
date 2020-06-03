using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
    public class Transaction : BaseEntity
    {
      [Required]
      [MaxLength(25)]
      public string TransactionType { get; set; }
      [Required]
      public decimal Amount { get; set; }
      [Required]
      public bool IsShared { get; set; }

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
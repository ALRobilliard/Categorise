using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
  public class TransactionTag
  {
    public Guid TransactionTagId { get; set; }
    public string TransactionTagName { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public Guid TransactionId { get; set; }
    [ForeignKey("TransactionForeignKey")]
    public Transaction Transaction { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey("UserForeignKey")]
    public User User { get; set; }
  }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
  public class TransactionTag : BaseEntity
  {
    [Required]
    [MaxLength(25)]
    public string TransactionTagName { get; set; }

    public Guid TransactionId { get; set; }
    [ForeignKey("TransactionForeignKey")]
    public Transaction Transaction { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey("UserForeignKey")]
    public User User { get; set; }
  }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
  public class TransactionParty : BaseEntity
  {
    [Required]
    [MaxLength(50)]
    public string TransactionPartyName { get; set; }

    public Guid TransactionId { get; set; }
    [ForeignKey("TransactionForeignKey")]
    public Transaction Transaction { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey("UserForeignKey")]
    public User User { get; set; }
  }
}
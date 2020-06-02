using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
  public class Account : BaseEntity
  {
    [Key]
    public Guid AccountId { get; set; }
    [Required]
    [MaxLength(25)]
    public string AccountName { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey("UserForeignKey")]
    public User User { get; set; }

    public List<Transaction> Transactions { get; set; }
  }
}
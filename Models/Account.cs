using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
  public class Account
  {
    public Guid AccountId { get; set; }
    public string AccountName { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey("UserForeignKey")]
    public User User { get; set; }

    public List<Transaction> Transactions { get; set; }
  }
}
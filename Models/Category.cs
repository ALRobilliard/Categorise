using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
    public class Category : BaseEntity
    {
      [Required]
      [MaxLength(25)]
      public string CategoryName { get; set; }

      public Guid UserId { get; set; }
      [ForeignKey("UserForeignKey")]
      public User User { get; set; }

      public List<Transaction> Transactions { get; set; }
    }
}
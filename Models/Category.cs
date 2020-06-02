using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
    public class Category
    {
      [Key]
      public Guid CategoryId { get; set; }
      [Required]
      [MaxLength(25)]
      public string CategoryName { get; set; }
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public DateTime CreatedOn { get; set; }
      [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
      public DateTime ModifiedOn { get; set; }

      public Guid UserId { get; set; }
      [ForeignKey("UserForeignKey")]
      public User User { get; set; }

      public List<Transaction> Transactions { get; set; }
    }
}
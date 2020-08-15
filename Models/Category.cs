using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
    /// <summary>
    /// Category entity model.
    /// </summary>
    public class Category : BaseEntity
    {
      /// <summary>
      /// Gets or sets the Category name.
      /// </summary>
      [Required]
      [MaxLength(25)]
      public string CategoryName { get; set; }

      /// <summary>
      /// Unqiue identifier for the owning user.
      /// </summary>
      public Guid UserId { get; set; }

      /// <summary>
      /// Owning user.
      /// </summary>
      [ForeignKey("UserForeignKey")]
      public User User { get; set; }

      /// <summary>
      /// List of transactions associated with this category.
      /// </summary>
      public List<Transaction> Transactions { get; set; }
    }
}
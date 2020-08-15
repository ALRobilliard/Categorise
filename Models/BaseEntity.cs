using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriseApi.Models
{
  /// <summary>
  /// Base entity model.
  /// </summary>
  public class BaseEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the created on date.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the modified on date.
    /// </summary>
    public DateTime ModifiedOn { get; set; }
  }
}
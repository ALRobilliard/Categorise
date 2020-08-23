using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriseApi.Dtos
{
  /// <summary>
  /// Data transfer object for Category.
  /// </summary>
  public class CategoryDto
  {
    /// <summary>
    /// Gets or sets the category ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    [Required]
    public string CategoryName { get; set; }
  }
}
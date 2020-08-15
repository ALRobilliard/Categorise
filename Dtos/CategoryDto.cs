using System;

namespace CategoriseApi.Dtos
{
  /// <summary>
  /// Data transfer unit for Category.
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
    public string CategoryName { get; set; }
  }
}
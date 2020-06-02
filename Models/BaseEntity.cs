using System;

namespace CategoriseApi.Models
{
  public class BaseEntity
  {
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
  }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoriseApi.Models
{
  public class TransactionNote
  {
    public Guid TransactionNoteId { get; set; }
    public string TransactionNoteSubject { get; set; }
    public string TransactionNoteBody { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public Guid TransactionId { get; set; }
    [ForeignKey("TransactionForeignKey")]    
    public Transaction Transaction { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey("UserForeignKey")]
    public User User { get; set; }
  }
}
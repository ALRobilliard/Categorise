using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CategoriseApi.Models
{
  public class CategoriseContext : DbContext
  {
    private readonly string _connectionString;

    public CategoriseContext()
    {
    }

    public CategoriseContext(DbContextOptions<CategoriseContext> options) : base (options)
    {
    }

    public CategoriseContext(string connectionString)
    {
      this._connectionString = connectionString;
    }

    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<TransactionNote> TransactionNotes { get; set; }
    public virtual DbSet<TransactionParty> TransactionParties { get; set; }
    public virtual DbSet<TransactionTag> TransactionTags { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseNpgsql(this._connectionString);
      }
    }

    public override int SaveChanges()
    {
      var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseEntity && (
          e.State == EntityState.Added ||
          e.State == EntityState.Modified));

      foreach (var entry in entries)
      {
        ((BaseEntity)entry.Entity).ModifiedOn = DateTime.Now;

        if (entry.State == EntityState.Added)
        {
          ((BaseEntity)entry.Entity).Id = Guid.NewGuid();
          ((BaseEntity)entry.Entity).CreatedOn = DateTime.Now;
        }
      }

      return base.SaveChanges();
    }
  }
}
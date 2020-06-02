using System;
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
  }
}
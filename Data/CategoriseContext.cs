using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Categorise.Data
{
    /// <summary>
    /// DbContext for Categorise.
    /// </summary>
    public class CategoriseContext : IdentityDbContext
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor CategoriseContext.
        /// </summary>
        public CategoriseContext()
        {
        }

        /// <summary>
        /// Constructor CategoriseContext.
        /// </summary>
        public CategoriseContext(DbContextOptions<CategoriseContext> options) : base(options)
        {
        }

        /// <summary>
        /// Constructor CategoriseContext.
        /// </summary>
        public CategoriseContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Account DbSet.
        /// </summary>
        public virtual DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Category DbSet.
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Config setting DbSet.
        /// </summary>
        public virtual DbSet<ConfigSetting> ConfigSettings { get; set; }

        /// <summary>
        /// Transaction DbSet.
        /// </summary>
        public virtual DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Transaction note DbSet.
        /// </summary>
        public virtual DbSet<TransactionNote> TransactionNotes { get; set; }

        /// <summary>
        /// Transaction party DbSet.
        /// </summary>
        public virtual DbSet<TransactionParty> TransactionParties { get; set; }

        /// <summary>
        /// Transaction tag DbSet.
        /// </summary>
        public virtual DbSet<TransactionTag> TransactionTags { get; set; }

        /// <summary>
        /// OnConfiguring method.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(this._connectionString);
            }
        }

        /// <summary>
        /// Handles methods to be executed on save of records.
        /// </summary>
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
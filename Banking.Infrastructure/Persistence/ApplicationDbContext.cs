using Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                            .HasOne(t => t.Account)
                            .WithMany(a => a.Transactions)
                            .HasForeignKey(t => t.AccountId)
                            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

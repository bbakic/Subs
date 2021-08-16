using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Subs.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseItem> ExpenseItems { get; set; }
        public virtual DbSet<Journal> Journals { get; set; }
        public virtual DbSet<JournalItem> JournalItems { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
        public virtual DbSet<ServiceTier> ServiceTiers { get; set; }
        public virtual DbSet<SubsVersion> SubsVersions { get; set; }
        public virtual DbSet<SubsVersionItem> SubsVersionItems { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<SubscriptionItem> SubscriptionItems { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Modus;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Croatian_CI_AS");

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Journal)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.JournalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Expenses_Journals");
            });

            modelBuilder.Entity<ExpenseItem>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Expense)
                    .WithMany(p => p.ExpenseItems)
                    .HasForeignKey(d => d.ExpenseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpenseItems_Expenses");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ExpenseItems)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpenseItems_Services");
            });

            modelBuilder.Entity<Journal>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Journals)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Journals_Customers");

                entity.HasOne(d => d.ServiceCategory)
                    .WithMany(p => p.Journals)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Journals_ServiceCategories");
            });

            modelBuilder.Entity<JournalItem>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Journal)
                    .WithMany(p => p.JournalItems)
                    .HasForeignKey(d => d.JournalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JournalItems_Journals");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.JournalItems)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JournalItems_Services");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasOne(d => d.ServiceCategory)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Services_ServiceCategories");
            });

            modelBuilder.Entity<ServiceTier>(entity =>
            {
                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceTiers)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceTiers_Services");
            });

            modelBuilder.Entity<SubsVersion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.SubsVersions)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubsVersions_Subscriptions");
            });

            modelBuilder.Entity<SubsVersionItem>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.SubsVersionItems)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubsVersionItems_Services");

                entity.HasOne(d => d.SubsVersion)
                    .WithMany(p => p.SubsVersionItems)
                    .HasForeignKey(d => d.SubsVersionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubsVersionItems_SubsVersions");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscriptions_Customers");

                entity.HasOne(d => d.ServiceCategory)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscriptions_ServiceCategories");
            });

            modelBuilder.Entity<SubscriptionItem>(entity =>
            {
                entity.HasOne(d => d.Service)
                    .WithMany(p => p.SubscriptionItems)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubscriptionItems_Services");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.SubscriptionItems)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubscriptionItems_Subscriptions");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

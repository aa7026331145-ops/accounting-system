using Microsoft.EntityFrameworkCore;
using AccountingSystem.Core.Entities;

namespace AccountingSystem.Data
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SupplierPayment> SupplierPayments { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }
        public DbSet<GeneralLedgerAccount> GeneralLedgerAccounts { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasMany(e => e.Invoices).WithOne(i => i.Customer).HasForeignKey(i => i.CustomerId).OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.Payments).WithOne(p => p.Customer).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Supplier entity
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasMany(e => e.PurchaseInvoices).WithOne(i => i.Supplier).HasForeignKey(i => i.SupplierId).OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.SupplierPayments).WithOne(p => p.Supplier).HasForeignKey(p => p.SupplierId).OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.Category).WithMany(c => c.Products).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Unit).WithMany(u => u.Products).HasForeignKey(e => e.UnitId).OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Configure Unit entity
            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            });

            // Configure Invoice entity
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.Customer).WithMany(c => c.Invoices).HasForeignKey(e => e.CustomerId).OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.Items).WithOne(i => i.Invoice).HasForeignKey(i => i.InvoiceId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.Payments).WithOne(p => p.Invoice).HasForeignKey(p => p.InvoiceId).OnDelete(DeleteBehavior.SetNull);
            });

            // Configure GeneralLedgerAccount entity
            modelBuilder.Entity<GeneralLedgerAccount>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AccountCode).IsRequired().HasMaxLength(20);
                entity.Property(e => e.AccountName).IsRequired().HasMaxLength(200);
            });

            // Configure JournalEntry entity
            modelBuilder.Entity<JournalEntry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EntryNumber).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.DebitAccount).WithMany(a => a.JournalEntries).HasForeignKey(e => e.DebitAccountId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
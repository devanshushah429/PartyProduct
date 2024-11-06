using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System;

namespace Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Party> Parties { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<PartyWiseProduct> PartyWiseProducts { get; set; } 
        
        public DbSet<ProductRate> ProductRates { get; set; }
        
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceWiseProduct> InvoiceWiseProducts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<Party>().ToTable("Parties");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<PartyWiseProduct>().ToTable("PartyWiseProducts");
            modelBuilder.Entity<ProductRate>().ToTable("ProductRates");
            modelBuilder.Entity<Invoice>().ToTable("Invoices");
            modelBuilder.Entity<InvoiceWiseProduct>().ToTable("InvoiceWiseProducts");*/
        }
    }
}

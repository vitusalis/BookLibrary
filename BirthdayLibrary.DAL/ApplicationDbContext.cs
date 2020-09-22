using Microsoft.EntityFrameworkCore;
using BirthdayLibrary.BLL.Models;
using System;


namespace BirthdayLibrary.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base() { }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:autonomousservicedb.database.windows.net,1433;" +
                "Initial Catalog=BirthdayLibrary;Persist Security Info=False;User ID=defaultuser;" +
                "Password=3I9fc0T%#axLk1;MultipleActiveResultSets=False;Encrypt=True;" +
                "TrustServerCertificate=False;Connection Timeout=30;", providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookAuthor>()
        .HasKey(bc => new { bc.BookId, bc.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(bc => bc.BookId);
            modelBuilder.Entity<BookAuthor>()
                .HasOne(bc => bc.Author)
                .WithMany(c => c.BookAuthors)
                .HasForeignKey(bc => bc.AuthorId);
        }

    }
}

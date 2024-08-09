using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost; Database=PRN231_Lab2; User Id=sa; Password=Abcd1234; TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Primary key
        modelBuilder.Entity<Author>().HasKey(a => a.AuthorId);
        modelBuilder.Entity<Book>().HasKey(b => b.BookId);
        modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });
        modelBuilder.Entity<Publisher>().HasKey(p => p.PubId);
        modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
        modelBuilder.Entity<User>().HasKey(u => u.UserId);
        // Relationship
        modelBuilder.Entity<User>()
            .HasOne<Role>(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
        modelBuilder.Entity<User>()
            .HasOne<Publisher>(u => u.Publisher)
            .WithMany(p => p.Users)
            .HasForeignKey(u => u.PubId);
        modelBuilder.Entity<Book>()
            .HasOne<Publisher>(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PubId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne<Book>(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);
        
        modelBuilder.Entity<BookAuthor>()
            .HasOne<Author>(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);
    }
}
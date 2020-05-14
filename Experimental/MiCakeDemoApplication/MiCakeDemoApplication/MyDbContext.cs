using MiCake.EntityFrameworkCore;
using MiCakeDemoApplication.Domain.BookBoundary.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace MiCakeDemoApplication
{
    public class MyDbContext : MiCakeDbContext
    {
        public virtual DbSet<Book> Books { get; set; }

        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //持久化值对象方法之一：使用EFCore的特性
            modelBuilder.Entity<Book>()
                .OwnsOne(s => s.Author);
        }
    }
}

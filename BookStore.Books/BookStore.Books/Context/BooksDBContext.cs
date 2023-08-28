using BookStore.Books.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Books.Context
{
    public class BooksDBContext : DbContext
    {
        public BooksDBContext(DbContextOptions<BooksDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<BookEntity> Books { get; set; }
    }
}

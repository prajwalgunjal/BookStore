using BookStore.User.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.User.Context
{
    public class User_DBContext : DbContext
    {
        public User_DBContext(DbContextOptions<User_DBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<UserEntity> User { get; set; }
    }
}

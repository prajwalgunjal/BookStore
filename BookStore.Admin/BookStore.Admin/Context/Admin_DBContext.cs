using BookStore.Admin.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Admin.Context
{
    public class Admin_DBContext : DbContext
    {
        public Admin_DBContext(DbContextOptions<Admin_DBContext> dbContextOptions) : base (dbContextOptions){ }

        public DbSet<AdminEntity> Admin { get; set; }
    }
}

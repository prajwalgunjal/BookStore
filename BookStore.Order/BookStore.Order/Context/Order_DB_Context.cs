using BookStore.Order.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Order.Context
{
    public class Order_DB_Context : DbContext
    {
        public Order_DB_Context(DbContextOptions<Order_DB_Context> dbContextOptions): base(dbContextOptions) { }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<WishListEntity> WishList { get; set; }
    }
}

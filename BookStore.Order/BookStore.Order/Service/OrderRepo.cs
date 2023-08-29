using BookStore.Order.Context;
using BookStore.Order.Entity;
using BookStore.Order.Interface;

namespace BookStore.Order.Service
{
    public class OrderRepo : IOrderRepo
    {
        private readonly Order_DB_Context order_DB_Context;
        private readonly IBookRepo bookRepo;
        private readonly IUserRepo userRepo;
        public OrderRepo(Order_DB_Context order_DB_Context, IBookRepo bookRepo, IUserRepo userRepo)
        {
            this.order_DB_Context = order_DB_Context;
            this.bookRepo = bookRepo;
            this.userRepo = userRepo;
        }
        //public async Task<OrderEntity> PlaceOrder(int userId, int bookId, int qty)
        //{
        //    OrderEntity order = new()
        //    {
        //        BookID = bookId,
        //        UserID = userId,
        //        Quantity = qty,
        //        Book = await bookRepo.GetBookDetails(bookId),
        //        User = new UserEntity()
        //    };

        //    order.OrderAmount = order.Book.Discount * qty;
        //    order_DB_Context.Add(order);
        //    order_DB_Context.SaveChanges();
        //    return order;
        //}
        public async Task<OrderEntity> PlaceOrder(string token, int bookId, int qty)
        {
            UserEntity user = await userRepo.GetUserDetails(token);

            OrderEntity order = new()
            {
                BookID = bookId,
                UserID = user.UserID,
                Quantity = qty,
                Book = await bookRepo.GetBookDetails(bookId),
                User = user
            };

            order.OrderAmount =  order.Book.Discount * qty;
            order_DB_Context.Add(order);
            order_DB_Context.SaveChanges();
            return order;
        }
    }
}

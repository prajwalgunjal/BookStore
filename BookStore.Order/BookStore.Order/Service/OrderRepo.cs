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


        // view order -> return OrderEntity with order amount and user, book details
        // delete order

        public async Task<List<OrderEntity>> GetOrders(int userID, string token)
        {
            List<OrderEntity> result = order_DB_Context.Orders.Where(x => x.UserID == userID).ToList();

            if (result != null)
            {
                foreach (OrderEntity order in result)
                {
                    order.Book = await bookRepo.GetBookDetails(order.BookID);
                    order.User = await userRepo.GetUserDetails(token);
                }
                return result;
            }
            return null;
        }

        public async Task<OrderEntity> GetOrdersByOrderID(int orderID, int userID, string token)
        {
            OrderEntity orderEntity = order_DB_Context.Orders.Where(x => x.OrderID == orderID && x.UserID == userID).FirstOrDefault();
            if (orderEntity != null)
            {
                orderEntity.Book = await bookRepo.GetBookDetails(orderEntity.BookID);
                orderEntity.User = await userRepo.GetUserDetails(token);

                return orderEntity;
            }
            return null;
        }

        public bool RemoveOrder(int orderID, int userID)
        {
            OrderEntity orderEntity = order_DB_Context.Orders.Where(x => x.OrderID == orderID && x.UserID == userID).FirstOrDefault();
            if (orderEntity != null)
            {
                order_DB_Context.Orders.Remove(orderEntity);
                order_DB_Context.SaveChanges();

                return true;
            }
            return false;
        }

    }
}

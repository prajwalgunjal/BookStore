using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IOrderRepo
    {
        //Task<OrderEntity> PlaceOrder(int userId, int bookId, int qty);
        Task<OrderEntity> PlaceOrder(string token, int bookId, int qty);
        Task<List<OrderEntity>> GetOrders(int userID, string token);
        Task<OrderEntity> GetOrdersByOrderID(int orderID, int userID, string token);
        public bool RemoveOrder(int orderID, int userID);
    }
}
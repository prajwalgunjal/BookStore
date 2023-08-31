using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IWishListRepo
    {
        Task<WishListEntity> AddWishList(int bookID, int userID, string token);
        Task<List<WishListEntity>> GetWishListByUserID(int userID);
        bool RemoveWishList(int bookID, int userID);
    }
}
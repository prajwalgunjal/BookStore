using BookStore.Order.Context;
using BookStore.Order.Entity;
using BookStore.Order.Interface;

namespace BookStore.Order.Service
{
    public class WishListRepo : IWishListRepo
    {
        private readonly Order_DB_Context orderDBContext;
        private readonly IBookRepo bookService;
        private readonly IUserRepo userServices;
        public WishListRepo(Order_DB_Context orderDBContext, IBookRepo bookService, IUserRepo userServices)
        {
            this.orderDBContext = orderDBContext;
            this.bookService = bookService;
            this.userServices = userServices;
        }

        public async Task<WishListEntity> AddWishList(int bookID, int userID, string token)
        {
            if (!orderDBContext.WishList.Any(x => x.UserID == userID && x.BookID == bookID))
            {
                WishListEntity wishList = new WishListEntity()
                {
                    BookID = bookID,
                    UserID = userID,
                    Book = await bookService.GetBookDetails(bookID),
                    User = await userServices.GetUserDetails(token)
                };
                orderDBContext.WishList.Add(wishList);
                orderDBContext.SaveChanges();
                return wishList;
            }
            return null;
        }

        public bool RemoveWishList(int bookID, int userID)
        {
            var result = orderDBContext.WishList.FirstOrDefault(x => x.BookID == bookID && x.UserID == userID);
            if (result != null)
            {
                orderDBContext.WishList.Remove(result);
                orderDBContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<List<WishListEntity>> GetWishListByUserID(int userID)
        {
            List<WishListEntity> wishList = (List<WishListEntity>)orderDBContext.WishList.Where(x => x.UserID == userID).ToList();
            if (wishList != null)
            {
                foreach (WishListEntity wish in wishList)
                {
                    wish.Book = await bookService.GetBookDetails(wish.BookID);

                }
                return wishList;
            }
            return null;
        }

    }
}

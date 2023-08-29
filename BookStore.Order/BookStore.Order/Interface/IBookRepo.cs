using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IBookRepo
    {
        Task<BookEntity> GetBookDetails(int id);
    }
}
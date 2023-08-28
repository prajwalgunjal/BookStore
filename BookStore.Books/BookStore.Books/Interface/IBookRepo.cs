using BookStore.Books.Entity;

namespace BookStore.Books.Interface
{
    public interface IBookRepo
    {
        BookEntity AddBook(BookEntity bookEntity);
        bool DeleteBook(int BookID);
        BookEntity GetBookByID(int BookID);
        public List<BookEntity> GetAllBooks();

    }
}
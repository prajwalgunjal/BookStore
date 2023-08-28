using BookStore.Books.Context;
using BookStore.Books.Entity;
using BookStore.Books.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Books.Service
{
    public class BookRepo : IBookRepo
    {

        private readonly BooksDBContext booksDBContext;
        private readonly IConfiguration _config;

        public BookRepo(BooksDBContext booksDBContext, IConfiguration config)
        {
            this.booksDBContext = booksDBContext;
            _config = config;   
        }

        public BookEntity AddBook(BookEntity bookEntity)
        {
            BookEntity NewBook = new BookEntity();
            NewBook.BookName = bookEntity.BookName;
            NewBook.Description = bookEntity.Description;
            NewBook.Author = bookEntity.Author;
            NewBook.Quantity = bookEntity.Quantity;
            NewBook.Discount = bookEntity.Discount;
            NewBook.Price = bookEntity.Price;
            booksDBContext.Books.Add(NewBook);
            booksDBContext.SaveChanges();

            return NewBook;
        }

        public bool DeleteBook(int BookID)
        {
            BookEntity booktoDelete = booksDBContext.Books.Where(x => x.BookID == BookID).FirstOrDefault();

            if (booktoDelete != null)
            {
                booksDBContext.Books.Remove(booktoDelete);
                booksDBContext.SaveChanges();

                return true;
            }

            return false;
        }

        public BookEntity GetBookByID(int BookID)
        {
            BookEntity books = booksDBContext.Books.Where(x => x.BookID == BookID).FirstOrDefault();

            if (books != null)
            {
                return books;
            }

            return null;
        }

        public List<BookEntity> GetAllBooks()
        {
            List<BookEntity> books = booksDBContext.Books.ToList();

            if (books != null)
            {
                return books;
            }

            return null;
        }

        public string GenerateToken(string email, int adminID)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",email),
                new Claim("AdminID",adminID.ToString()),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

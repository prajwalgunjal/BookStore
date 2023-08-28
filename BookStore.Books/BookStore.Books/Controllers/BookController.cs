using BookStore.Books.Entity;
using BookStore.Books.Interface;
using BookStore.Books.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookRepo BookRepo { get; set; }
        public BookController(IBookRepo bookRepo) {
        this.BookRepo = bookRepo;
        }

        [HttpPost]
        [Route("AddBook")]
        public ActionResult AddBook(BookEntity bookEntity)
        {
            BookEntity book = BookRepo.AddBook(bookEntity);
            if(book != null)
            {
                return Ok(new ResponseModel<BookEntity> { Status = true, Message = "Book Added Successfully",Data= book }) ;
            }
            return BadRequest(new ResponseModel<BookEntity> { Status = false, Message = "Book not Added", Data = null });

        }
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int BookID)
        {
            bool result = BookRepo.DeleteBook(BookID);
            if (result)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Book Deleted" });
            }

            return BadRequest(new ResponseModel<string> { Status = false, Message = "Not Deleted" });
        }



        [HttpGet("GetbookById")]
        public IActionResult GetBookByID(int bookID)
        {
            BookEntity book = BookRepo.GetBookByID(bookID);
            if (book != null)
            {
                return Ok(new ResponseModel<BookEntity> { Status = true, Message = "Book Displayed", Data = book });
            }
            return NotFound(new ResponseModel<string> { Status = false, Message = "Book not found" });
        }

        [HttpPost]
        [Route("GetAllBooks")]

        public ActionResult GetAllBooks()
        {
            List<BookEntity> books = BookRepo.GetAllBooks().ToList();
            if(books != null)
            {
                return Ok (new ResponseModel<List<BookEntity>> { Status=true,Message="Displayed",Data = books }) ;
            }
            return BadRequest(new ResponseModel<List<BookEntity>> { Status=false, Message ="Not displayed" ,Data= null });    
        }
    }
}

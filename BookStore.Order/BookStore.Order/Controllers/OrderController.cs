using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBookRepo bookRepo;
        private readonly IOrderRepo orderRepo;
        private readonly IUserRepo userRepo;
        public OrderController(IBookRepo bookRepo, IOrderRepo orderRepo, IUserRepo userRepo)
        {
            this.bookRepo = bookRepo;
            this.orderRepo = orderRepo;
            this.userRepo = userRepo;   
        }

        [HttpGet("getBookDetails")]
        public async Task<IActionResult> GetBookDetails(int bookID)
        {
            BookEntity book = await bookRepo.GetBookDetails(bookID);

            if (book != null)
            {
                return Ok(book);
            }
            return BadRequest("unable to get book details");
        }

        [HttpGet("getUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            string token = Request.Headers.Authorization.ToString(); // token will have "Bearer " which we need to remove
            token = token.Substring("Bearer ".Length); // now we will only have the actual jwt token - without Bearer and a space
            UserEntity user = await userRepo.GetUserDetails(token);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("unable to get user");
        }
        [Authorize]
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder (int bookID ,int Qty)
        {
            string token = Request.Headers.Authorization.ToString(); // token will have "Bearer " which we need to remove
            token = token.Substring("Bearer ".Length); // now we will only have the actual jwt token - without Bearer and a space
            OrderEntity orderEntity = await orderRepo.PlaceOrder(token,bookID,Qty);
            if(orderEntity != null)
            {
                return Ok(orderEntity);
            }
            return BadRequest("Unable to place order");
        }

    }
}

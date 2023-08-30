using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Model;
using BookStore.Order.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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


        [Authorize]
        [HttpGet("getOrders")]
        public async Task<IActionResult> GetOrders()
        {
            string token = Request.Headers.Authorization.ToString(); // token will have "Bearer " which we need to remove
            token = token.Substring("Bearer ".Length);

            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));

            List<OrderEntity> orderEntity = await orderRepo.GetOrders(userID, token);
            if (orderEntity!= null)
            {
                return Ok(new ResponseModel { IsSucess = true, Message = "Orders Displyed", Data = orderEntity });
            }
           
            return BadRequest(new ResponseModel { IsSucess = false, Message = "unable to get the data", Data = null });
        }


        [Authorize]
        [HttpGet("getOrderByOrderID")]
        public async Task<IActionResult> GetOrdersByOrderID(int orderID)
        {
            string token = Request.Headers.Authorization.ToString();
            token = token.Substring("Bearer ".Length);

            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));

            OrderEntity order = await orderRepo.GetOrdersByOrderID(orderID, userID, token);
            if (order != null)
            {
                return Ok(new ResponseModel { IsSucess = true, Message = "succesfully get order details", Data = order });
            }

            return BadRequest(new ResponseModel{ IsSucess = false, Message = "order not found" });
        }

        [Authorize]
        [HttpDelete("removeOrder")]
        public IActionResult RemoveOrder(int orderID)
        {
            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));
            bool isRemove = orderRepo.RemoveOrder(orderID, userID);
            if (isRemove)
            {
                return Ok(new ResponseModel { IsSucess = true, Message = "succesfully removed order" });
            }
            return BadRequest(new ResponseModel { IsSucess = false, Message = "unable to remove order" });
        }

    }
} 
// 10 3 sharing  // 1 mennth deposite 

// 9702077768 

//14 2

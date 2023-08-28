using BookStore.Admin.Entity;
using BookStore.Admin.Interface;
using BookStore.Admin.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo adminRepo;

        public AdminController(IAdminRepo adminRepo) {
        this.adminRepo = adminRepo;
        }

        [HttpPost]
        [Route("/Register")]
        public IActionResult AddAdmin(AdminEntity adminEntity)
        {
            AdminEntity admin = adminRepo.RegsiterAdmin(adminEntity);

            if (admin != null)
            {
                return Ok(new ResponseModel<AdminEntity> { Status = true, Message = "Registered succesfully", Data = admin });
            }

            return BadRequest(new ResponseModel<string> { Status = false, Message = "unsuccesfull to Register" });
        }

        [HttpPost]
        [Route("/Login")]
        public IActionResult AdminLogin(string email, string password)
        {
            var admin = adminRepo.AdminLogin(email,password);

            if (admin != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "login succesfull", Data = admin });
            }

            return BadRequest(new ResponseModel<string> { Status = false, Message = "unsuccesfull to login" });

        }
    }
}

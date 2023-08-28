using BookStore.User.Context;
using BookStore.User.Entity;
using BookStore.User.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace BookStore.User.Services
{
    public class UserRepo : IUserRepo
    {

        private readonly User_DBContext _dbContext;
        private readonly IConfiguration _config;
        public UserRepo()
        {

        }
        public UserRepo(User_DBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _config = configuration;
        }
        public UserEntity AddUser(UserEntity user)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Email = user.Email;
            userEntity.Password = EncodePasswordToBase64( user.Password);
            userEntity.Address = user.Address;
            _dbContext.Add(userEntity);
            _dbContext.SaveChanges();
            return user;
        }

        public string LoginUser(string email,string password)
        {
            string encodedPassword = EncodePasswordToBase64(password);
            UserEntity result = _dbContext.User.Where(x => x.Email == email && x.Password == encodedPassword).FirstOrDefault();
            if (result != null)
            {
                return GenerateToken(result.Email, result.UserID);
            }

            return null;
        }
        public string ForgetPassword(string email)
        {
            var result = _dbContext.User.Where(x => x.Email == email).FirstOrDefault();

            if (result != null)
            {
                string token = GenerateToken(result.Email, result.UserID);

                return token;
            }

            return null;
        }

        public UserEntity ResetPassword(string password, string confirmPassword, string email)
        {
            var result = _dbContext.User.Where(x => x.Email == email).FirstOrDefault();
            if (result != null)
            {
                result.Password = confirmPassword;
                _dbContext.SaveChanges();
                return result;
            }
            return null;
        }
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public UserEntity GetUserProfile(int userID)
        {
            var result = _dbContext.User.Where(x => x.UserID == userID).FirstOrDefault();
            return result;
        }

        private string GenerateToken(string email, int userID)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",email),
                new Claim("UserID",userID.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string SendEmail(string emailTo, string token)
        {
            string emailFrom = "prajwalgunjal86@gmail.com";
            string subject = "Token genrated ";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587); // gmail smtp
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential("prajwalgunjal86@gmail.com", "midkdytiiwoqrwro");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;

            smtpClient.Send(emailFrom, emailTo, subject, token);

            return emailTo;
        }
    }
}

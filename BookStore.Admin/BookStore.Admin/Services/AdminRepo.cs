using BookStore.Admin.Context;
using BookStore.Admin.Entity;
using BookStore.Admin.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace BookStore.Admin.Services
{
    public class AdminRepo : IAdminRepo
    {
        private readonly Admin_DBContext admin_DBContext;
        private readonly IConfiguration _config;
        public AdminRepo(Admin_DBContext admin_DBContext, IConfiguration _config)
        {
            this.admin_DBContext = admin_DBContext;
            this._config = _config;
        }

        public AdminEntity RegsiterAdmin(AdminEntity admin)
        {
            AdminEntity newadminEntity = new AdminEntity();
            newadminEntity.FirstName= admin.FirstName;
            newadminEntity.LastName= admin.LastName;
            newadminEntity.Email= admin.Email;
            newadminEntity.Password= EncodePasswordToBase64(admin.Password);
            admin_DBContext.Admin.Add(newadminEntity);
            admin_DBContext.SaveChanges();
            return admin;
        }

        public string AdminLogin(string email, string password)
        {
            try
            {
                string encodedPassword = EncodePasswordToBase64(password);
                var checkEmail = admin_DBContext.Admin.FirstOrDefault(x => x.Email == email);
                var checkPassword = admin_DBContext.Admin.FirstOrDefault(x => x.Password == encodedPassword);

                if (checkEmail != null && checkPassword != null)
                {
                    var token = GenerateToken(checkEmail.Email, checkEmail.AdminId);
                    return token;
                }

                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
    }
}

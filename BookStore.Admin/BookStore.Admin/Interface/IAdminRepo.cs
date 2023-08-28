using BookStore.Admin.Entity;

namespace BookStore.Admin.Interface
{
    public interface IAdminRepo
    {
        AdminEntity RegsiterAdmin(AdminEntity adminEntity);
        public string AdminLogin(string email, string password);
    }
}
using BookStore.User.Entity;

namespace BookStore.User.Interface
{
    public interface IUserRepo
    {
        UserEntity AddUser(UserEntity user);
        public string ForgetPassword(string email);
        public string LoginUser(string email, string password);
        UserEntity GetUserProfile(int userID);
        UserEntity ResetPassword(string password, string confirmPassword, string email);
    }
}
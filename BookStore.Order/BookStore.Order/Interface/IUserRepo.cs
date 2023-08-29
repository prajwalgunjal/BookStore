using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IUserRepo
    {
        Task<UserEntity> GetUserDetails(string token);
    }
}
using QAgentApi.Model;

namespace QAgentApi.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> InsertNewUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> GetUserById(int userId);
        Task DeleteUserById(int userId);
        Task<User> GetUserByEmail(string email);
    }
}

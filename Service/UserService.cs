using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Service
{
    public class UserService
    {
        // Dependency Injection
        private readonly IUserRepository _userRepo;
        // CONSTRUCTOR
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // METHODS

        // Get User by Id
        public async Task<User?> GetUserById(int userId)
        {
            return await _userRepo.GetUserById(userId);
        }

        // Get User by Email
        public virtual async Task<User?> GetUserByEmail(string email)
        {
            return await _userRepo.GetUserByEmail(email);
        }

        // Add User
        public async Task<User> AddUser(User user)
        {
            return await _userRepo.InsertNewUser(user);
        }

        // Update User
        public async Task<User> UpdateUser(User user)
        {
            return await _userRepo.UpdateUser(user);
        }

        // Delete User by Id
        public async Task DeleteUserById(int userId)
        {
            await _userRepo.DeleteUserById(userId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using QAgentApi.Data;
using QAgentApi.Model;

namespace QAgentApi.Service
{
    public class UserService
    {
        // DB permission
        private readonly AppDBContext _context;
        // CONSTRUCTOR
        public UserService(AppDBContext context)
        {
            _context = context;
        }

        // METHODS

        // get all users
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // Add User
        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using QAgentApi.Model;

namespace QAgentApi.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
    }
}

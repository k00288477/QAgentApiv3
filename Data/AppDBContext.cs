using Microsoft.EntityFrameworkCore;
using QAgentApi.Model;

namespace QAgentApi.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Organisation> Organisations => Set<Organisation>();
        public DbSet<TestSuite> TestSuites => Set<TestSuite>();
        public DbSet<TestCase> TestCases => Set<TestCase>();
        public DbSet<TestStep> TestSteps => Set<TestStep>();
        public DbSet<ExecutionRun> ExecutionRuns => Set<ExecutionRun>();
        public DbSet<ExecutionReport> ExecutionReports => Set<ExecutionReport>();
        public DbSet<Comment> Comments => Set<Comment>();
    }
}

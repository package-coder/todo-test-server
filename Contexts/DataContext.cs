using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using todo_test_server.Models;

namespace todo_test_server.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}

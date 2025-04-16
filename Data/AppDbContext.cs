using Microsoft.EntityFrameworkCore;

namespace TodoApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
    }
}
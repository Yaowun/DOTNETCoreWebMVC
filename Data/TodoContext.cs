using Microsoft.EntityFrameworkCore;
using DOTNETCoreWebMVC.Models;

namespace DOTNETCoreWebMVC.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<TodoModel> TodoModel { get; set; }
    }
}

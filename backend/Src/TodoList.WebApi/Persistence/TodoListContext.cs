using Microsoft.EntityFrameworkCore;
using TodoList.WebApi.Models;

namespace TodoList.WebApi.Persistence
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options) { }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem { Id = 1, Title = "Grocery Shopping", IsComplete = false, Description = "Buy milk, eggs, and bread" },
                new TodoItem { Id = 2, Title = "Pay Bills", IsComplete = true, Description = "Pay electricity and internet bills" }
            );
        }
    }
}

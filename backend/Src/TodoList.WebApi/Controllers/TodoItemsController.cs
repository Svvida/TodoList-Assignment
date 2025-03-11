using Microsoft.AspNetCore.Mvc;
using TodoList.WebApi.Models;
using TodoList.WebApi.Repositories;

namespace TodoList.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemsController(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems = await _repository.GetAllAsync();
            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _repository.GetByIdAsync(id);
            if (todoItem is null)
                return NotFound(new { message = "Todo item not found" });

            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem([FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, [FromBody] TodoItem todoItem)
        {
            if (id != todoItem.Id)
                return BadRequest(new { message = "Id in URL does not match item Id" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem is null)
                return NotFound(new { message = "Todo item not found" });

            await _repository.UpdateAsync(todoItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem is null)
                return NotFound(new { message = "Todo item not found" });

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}

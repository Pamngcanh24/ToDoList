using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            // Thêm dữ liệu mẫu nếu chưa có
            if (!_context.Todos.Any())
            {
                _context.Todos.Add(new TodoItem { Id = 1, Name = "Học REST API", IsComplete = false });
                _context.SaveChanges();
            }
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            return Ok(await _context.Todos.ToListAsync());
        }

        // GET: api/todo/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodo([FromBody] TodoItem todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        // PUT: api/todo/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoItem updatedTodo)
        {
            if (id != updatedTodo.Id) return BadRequest();
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();
            todo.Name = updatedTodo.Name;
            todo.IsComplete = updatedTodo.IsComplete;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/todo/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
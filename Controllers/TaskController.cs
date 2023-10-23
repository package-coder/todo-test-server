using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_test_server.Contexts;
using todo_test_server.Models;

namespace todo_test_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly DataContext _context;
        public TaskController(DataContext context)
        {
            _context = context;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateTask(int id, Models.Task task)
        {
            task.Id = id;
            _context.Tasks.Attach(task);
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpPost("todo/{todoId}")]
        public async Task<ActionResult<Todo>> AddTodoTask(int todoId, Models.Task task)
        {
            var todo = await _context.Todos.Include(todo => todo.Tasks).Where(todo => todo.Id == todoId).FirstAsync();

            if (todo == null)
            {
                return BadRequest();
            }

            task.TodoId = todo.Id;

            todo.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("AddTodoTask", new { id = task.Id }, task);
        }
    }
}

using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_test_server.Contexts;
using todo_test_server.Models;

namespace todo_test_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly DataContext _context;
        public TodoController(DataContext context)
        {
            _context = context;
        }

        // GET: TodoController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodo([FromQuery(Name = "tagIdFilter")] int? tagIdFilter, [FromQuery(Name = "search")] string? search)
        {
            var todos = await _context.Todos
                .Include(todo => todo.Tasks)
                .Include(todo => todo.Tags)
                .Where(todo => search == null || todo.Name.Contains(search))
                .Where(todo => tagIdFilter == null || todo.Tags.Any(tag => tag.Id == tagIdFilter))
                .ToListAsync();

            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _context.Todos
                .Include(todo => todo.Tasks)
                .Include(todo => todo.Tags)
                .Where(todo => todo.Id == id)
                .FirstAsync();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> SaveTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("SaveTodo", new { id = todo.Id }, todo);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> ArchivedTodo(int id, [FromQuery(Name = "archive")] bool archive)
        {
            var todo = await _context.Todos.FindAsync(id);

            if(todo != null)
            {
               todo.IsArchived = archive;
               await _context.SaveChangesAsync();
            }

            return Ok(todo);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<Todo>> UpdateTodo(int id, Todo todo)
        {
            todo.Id = id;
            _context.Todos.Attach(todo);
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(todo);
        }
    }
}

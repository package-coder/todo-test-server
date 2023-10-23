using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using todo_test_server.Contexts;
using todo_test_server.Models;

namespace todo_test_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly DataContext _context;

        public TagController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Tag>> GetAllTags([FromQuery(Name = "activeTags")] bool activeTags)
        {
            if(!activeTags)
                return Ok(await _context.Tags.ToListAsync());

            var tags = await _context.Tags
                .Include(tag => tag.Todos)
                .Where(tag => tag.Todos.Any(todo => !todo.IsArchived))
                .ToListAsync();
            return Ok(tags);
        }

        [HttpPost("{tagId}/todo/{todoId}")]
        public async Task<ActionResult<Tag>> TagTodo(int tagId, int todoId)
        {
            var todo = await _context.Todos.Include(todo => todo.Tags).Where(todo => todo.Id == todoId).FirstAsync();

            if (todo == null)
            {
                return BadRequest();
            }

            var tag = await _context.Tags.Include(tag => tag.Todos).Where(tag => tag.Id == tagId).FirstAsync();

            if (tag == null)
            {
                return BadRequest();
            }

            todo.Tags.Add(tag);
            tag.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("TagTodo", new { id = tag.Id }, tag);
        }

        [HttpPost("todo/{todoId}")]
        public async Task<ActionResult<Tag>> TagTodo(int todoId, Tag tag)
        {
            var todo = await _context.Todos.Include(todo => todo.Tags).Where(todo => todo.Id == todoId).FirstAsync();

            if (todo == null)
            {
                return BadRequest();
            }

            todo.Tags.Add(tag);
            tag.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("TagTodo", new { id = tag.Id }, tag);
        }


        [HttpDelete("{tagId}/todo/{todoId}")]
        public async Task<ActionResult<Tag>> UntagTodo(int tagId, int todoId)
        {
            var todo = await _context.Todos.Include(todo => todo.Tags).Where(todo => todo.Id == todoId).FirstAsync();

            if (todo == null)
            {
                return BadRequest();
            }

            var tag = todo.Tags.Where(tag => tag.Id == tagId).First();
            
            if (tag == null)
            {
                return BadRequest();
            }

            todo.Tags.Remove(tag);
            tag.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("UntagTodo", new { id = tag.Id }, tag);
        }
    }
}

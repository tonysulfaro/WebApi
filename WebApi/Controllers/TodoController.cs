using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAPI.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase {
        private readonly TodoDBContext _context;

        public TodoController(TodoDBContext context) {
            _context = context;
        }

        // GET: api/<TodoController>
        /// <summary>
        /// Gets all Todo Items.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Todo/
        ///
        /// </remarks>
        /// <returns>A list of Todo Items.</returns>
        /// <response code="200">Returns the items</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() {
            return await _context.Todos.ToListAsync();
        }

        // GET api/<TodoController>/5
        /// <summary>
        /// Gets a single Todo Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Todo/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Returns the item</response>
        /// <response code="404">If the item is not found</response>  
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id) {
            var todoItem = await _context.Todos.FindAsync(id);

            if (todoItem == null) {
                return NotFound();
            }

            return todoItem;
        }

        // POST api/<TodoController>
        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item) {
            _context.Todos.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT api/<TodoController>/5
        /// <summary>
        /// Updates a specific TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>None if successful</returns>
        /// <response code="204">Todo Item was updated</response>
        /// <response code="400">If the item is null</response> 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem item) {
            if (id != item.Id) {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<TodoController>/5
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Todo/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="204">Todo Item was deleted</response>
        /// <response code="404">If the item is not found</response> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id) {
            var todoItem = await _context.Todos.FindAsync(id);

            if (todoItem == null) {
                return NotFound();
            }

            _context.Todos.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

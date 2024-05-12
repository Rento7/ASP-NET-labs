using Microsoft.AspNetCore.Mvc;
using TodoService;

namespace TodoControllerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Get all TodoItems.
        /// </summary>
        /// <returns>all todos</returns>
        /// <response code="200">Returns the requested TodoItem</response>
        /// <response code="404">No items were found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ITodoItem>>> GetTodoItems()
        {
            var todos = await _todoService.GetAsync();

            if (todos == null)
                return NotFound();

            return Ok(todos);
        }

        /// <summary>
        /// Get the requested TodoItem by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the requested TodoItem</returns>
        /// <response code="200">Returns the requested todos</response>
        /// <response code="404">No items were found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ITodoItem>>> GetTodoItem(long id)
        {
            var todo = await _todoService.GetAsync(id);

            if (todo == null)
                return NotFound();

            return Ok(todo);

        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": false
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created TodoItem</response>
        /// <response code="400">The TodoItem is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTodoItem(TodoItem item)
        {
            if (item == null)
                return BadRequest();

            if (!_todoService.Add(item))
                return BadRequest();

            return CreatedAtAction(nameof(CreateTodoItem), new { id = item.Id }, item);
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Updates a specific TodoItem</response>
        /// <response code="400">Ids do not coincide</response>
        /// <response code="404">TodoItem was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItem todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            if (!_todoService.Update(id, todo))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Item was deleted successfully</response>
        /// <response code="404">Item was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (!_todoService.Remove(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TodoService;

namespace TodoControllerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddSingleton<ITodoService, TodoService.TodoService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            RouteGroupBuilder todoItems = app.MapGroup("/todoitems");
            todoItems.MapGet("/", GetTodoItems);
            todoItems.MapGet("/{id}", GetTodoItem);
            todoItems.MapPost("/", CreateTodoItem);
            todoItems.MapPut("/{id}", UpdateTodoItem);
            todoItems.MapDelete("/{id}", DeleteTodoItem);
            app.Run();
        }


        /// <summary>
        /// Get all TodoItems.
        /// </summary>
        /// <returns>all todos</returns>
        /// <response code="200">Returns the requested TodoItem</response>
        /// <response code="404">No items were found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        static async Task<IResult> GetTodoItems(ITodoService todoService)
        {
            var todos = await todoService.GetAsync();

            if (todos == null)
                return TypedResults.NotFound();

            return TypedResults.Ok(todos);
        }

        /// <summary>
        /// Get the requested TodoItem by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the requested TodoItem</returns>
        /// <response code="200">Returns the requested todos</response>
        /// <response code="404">No items were found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        static async Task<IResult> GetTodoItem(long id, ITodoService todoService)
        {
            var todo = await todoService.GetAsync(id);

            if (todo == null)
                return TypedResults.NotFound();

            return TypedResults.Ok(todo);

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        static async Task<IResult> CreateTodoItem(TodoItem item, ITodoService todoService)
        {
            if (item == null)
                return TypedResults.BadRequest();

            if (!todoService.Add(item))
                return TypedResults.BadRequest();

            return TypedResults.CreatedAtRoute(item, nameof(CreateTodoItem), new { id = item.Id });
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        static async Task<IResult> UpdateTodoItem(long id, TodoItem todo, ITodoService todoService)
        {
            if (id != todo.Id)
            {
                return TypedResults.BadRequest();
            }

            if (!todoService.Update(id, todo))
            {
                return TypedResults.NotFound();
            }

            return TypedResults.NoContent();
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Item was deleted successfully</response>
        /// <response code="404">Item was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        static async Task<IResult> DeleteTodoItem(long id, ITodoService todoService)
        {
            if (!todoService.Remove(id))
            {
                return TypedResults.NotFound();
            }

            return TypedResults.NoContent();
        }
    }
}

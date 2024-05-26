using AspLab3MinimalApiEntityFramework.Data;
using AspLab3MinimalApiEntityFramework.Models;
using AspLab3MinimalApiEntityFramework.Utility;
using Azure;
using System.Collections.Generic;

namespace AspLab3MinimalApiEntityFramework
{
    public static class TodoistApi
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>all users</returns>
        /// <response code="200">Returns all users</response>
        /// <response code="404">No users were found</response>
        public static async Task<IResult> GetAllUsers(ITodoistRepository repository)
        {
            var response = await repository.GetAllUsersAsync();

            var result = new ResponseDataModel<IEnumerable<UserDto>>()
            {
                Data = response.Data.ToDto(),
                Id = response.Id,
                StatusCode = response.StatusCode,
                Message = response.Message
            };

            return ResponseModelConverter.Convert(result);
        }

        /// <summary>
        /// Get specific user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>specific user by id</returns>
        /// <response code="200">Returns the requested user </response>
        /// <response code="404">User was not found</response>
        public static async Task<IResult> GetUserById(int id, ITodoistRepository repository)
        {
            var response = await repository.GetUserByIdAsync(id);

            var result = new ResponseDataModel<UserDto>()
            {
                Data = response.Data.ToDto(),
                Id = response.Id,
                StatusCode = response.StatusCode,
                Message = response.Message
            };

            return ResponseModelConverter.Convert(result);
        }

        /// <summary>
        /// Get all todos
        /// </summary>
        /// <returns>all todos</returns>
        /// <response code="200">Returns all todos</response>
        /// <response code="404">No todos were found</response>
        public static async Task<IResult> GetAllTodos(ITodoistRepository repository)
        {
            var response = await repository.GetAllTodosAsync();

            var result = new ResponseDataModel<IEnumerable<TodoDto>>()
            {
                Data = response.Data.ToDto(),
                Id = response.Id,
                StatusCode = response.StatusCode,
                Message = response.Message
            };

            return ResponseModelConverter.Convert(result);
        }

        /// <summary>
        /// Get all todos of specific user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>all todos of user</returns>
        /// <response code="200">Returns the requested todos of specific user</response>
        /// <response code="404">No todos were found</response>
        public static async Task<IResult> GetAllTodosByUser(int userId, ITodoistRepository repository)
        {
            var response = await repository.GetAllTodosByUserAsync(userId);

            var result = new ResponseDataModel<IEnumerable<TodoDto>>()
            {
                Data = response.Data.ToDto(),
                Id = response.Id,
                StatusCode = response.StatusCode,
                Message = response.Message
            };

            return ResponseModelConverter.Convert(result);
        }

        /// <summary>
        /// Get specific todo by id
        /// </summary>
        /// <param name="id">todo id</param>
        /// <returns>specific todo</returns>
        /// <response code="200">Returns the requested todo</response>
        /// <response code="404">Todo was not found</response>
        public static async Task<IResult> GetTodoById(int id, ITodoistRepository repository)
        {
            var response = await repository.GetTodoByIdAsync(id);

            var result = new ResponseDataModel<TodoDto>()
            {
                Data = response.Data.ToDto(),
                Id = response.Id,
                StatusCode = response.StatusCode,
                Message = response.Message
            };

            return ResponseModelConverter.Convert(result);
        }

        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A newly created user with settings</returns>
        /// <response code="201">Created successfully</response>
        /// <response code="400">Db error</response>
        public static async Task<IResult> CreateUser(UserDto user, ITodoistRepository repository)
        {
            if (user == null)
                return TypedResults.BadRequest();

            var result = await repository.CreateUserAsync(user.FromDto());
            return ResponseModelConverter.Convert(result, $"/users/{result.Id}");
        }

        /// <summary>
        /// Create a todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>A newly created todo</returns>
        /// <response code="201">Created successfully</response>
        /// <response code="400">Db error</response>
        public static async Task<IResult> CreateTodo(TodoDto todo, ITodoistRepository repository)
        {
            if (todo == null)
                return TypedResults.BadRequest();

            var result = await repository.CreateTodoAsync(todo.FromDto());
            return ResponseModelConverter.Convert(result, $"/todos/{result.Id}");
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns>An updated user</returns>
        /// <response code="200">Updated successfully</response>
        /// <response code="400">Db error</response>
        /// <response code="404">User was not found</response>
        public static async Task<IResult> UpdateUser(int userId, UserDto user, ITodoistRepository repository)
        {
            if (user == null)
                return TypedResults.BadRequest();

            user.SetId(userId);

            var result = await repository.UpdateUserAsync(user.FromDto());
            return ResponseModelConverter.Convert(result);
        }

        /// <summary>
        /// Updates todo
        /// </summary>
        /// <param name="todoId"></param>
        /// <param name="todo"></param>
        /// <returns>An updated todo</returns>
        /// <response code="200">Updated successfully</response>
        /// <response code="400">Db error</response>
        /// <response code="404">Todo was not found</response>
        public static async Task<IResult> UpdateTodo(int todoId, TodoDto todo, ITodoistRepository repository)
        {
            if (todo == null)
                return TypedResults.BadRequest();

            todo.SetId(todoId);

            var result = await repository.UpdateTodoAsync(todo.FromDto());
            return ResponseModelConverter.Convert(result);
        }

        /// <summary>
        /// Deletes user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="204">User was deleted successfully</response>
        /// <response code="400">Db error</response>
        /// <response code="404">User was not found</response>
        public static async Task<IResult> DeleteUser(int userId, ITodoistRepository repository)
        {
            var result = await repository.DeleteUserAsync(userId);
            return ResponseModelConverter.Convert(result);
        }

        /// <summary>
        /// Deletes todo by id
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns></returns>
        /// <response code="204">Todo was deleted successfully</response>
        /// <response code="400">Db error</response> 
        /// <response code="404">Todo was not found</response>
        public static async Task<IResult> DeleteTodo(int todoId, ITodoistRepository repository)
        {
            var result = await repository.DeleteTodoAsync(todoId);
            return ResponseModelConverter.Convert(result);
        }
    }
}

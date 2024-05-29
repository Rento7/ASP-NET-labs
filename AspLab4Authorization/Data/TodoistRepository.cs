using AspLab4Authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace AspLab4Authorization.Data
{
    public class TodoistRepository : ITodoistRepository, IDisposable
    {
        TodoistContext _context;
        bool _disposed = false;
        public TodoistRepository(TodoistContext context)
        {
            _context = context;
        }

        public async Task<IResponseDataModel<IEnumerable<User>>> GetAllUsersAsync()
        {
            return new ResponseDataModel<IEnumerable<User>>
            {
                Success = true,
                Data = await _context.Users.Include(e => e.Todos).ToListAsync(),
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<IResponseDataModel<User>> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.Include(e => e.Todos).FirstOrDefaultAsync(e => e.Id == id);
            return user != null ?
                    new ResponseDataModel<User>
                    { 
                        Success = true, 
                        Data = user, 
                        StatusCode = StatusCodes.Status200OK 
                    } :
                    new ResponseDataModel<User> 
                    { 
                        Success = false, 
                        Message = "User not found", 
                        StatusCode = StatusCodes.Status404NotFound
                    };
        }

        public async Task<IResponseDataModel<IEnumerable<Todo>>> GetAllTodosAsync()
        {
            return new ResponseDataModel<IEnumerable<Todo>>
            {
                Success = true,
                Data = await _context.Todos.ToListAsync(),
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<IResponseDataModel<IEnumerable<Todo>>> GetAllTodosByUserAsync(int userId)
        {
            return new ResponseDataModel<IEnumerable<Todo>>
            {
                Success = true,
                Data = await _context.Todos.Where(e => e.UserId == userId).ToListAsync(),
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<IResponseDataModel<Todo>> GetTodoByIdAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            return todo != null ?
                new ResponseDataModel<Todo> 
                {
                    Success = true,
                    Data = todo,
                    StatusCode = StatusCodes.Status200OK
                } :
                new ResponseDataModel<Todo> 
                {
                    Success = false, 
                    Message = "Todo not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
        }

        public async Task<IResponseModel> CreateUserAsync(User user)
        {
            user.Id = default;

            var userSettings = user.Settings;
            user.Settings = null;

            _context.Users.Add(user);

            if (await _context.SaveChangesAsync() == 0) 
            {
                new ResponseModel
                {
                    Success = false,
                    Message = "Unable to create user",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (userSettings == null)
                userSettings = new UserSettings();

            userSettings.Id = default;
            userSettings.UserId = user.Id;
            _context.UserSettings.Add(userSettings);

            return await _context.SaveChangesAsync() == 1 ?
                new ResponseModel 
                { 
                    Success = true, 
                    Id = user.Id,
                    StatusCode = StatusCodes.Status201Created
                } :
                new ResponseModel 
                { 
                    Success = false, 
                    Message = "Unable to create user",
                    StatusCode = StatusCodes.Status400BadRequest
                };
        }
        
        public async Task<IResponseModel> CreateTodoAsync(Todo todo)
        {
            todo.Id = default;

            _context.Todos.Add(todo);

            return await _context.SaveChangesAsync() == 1 ?
                new ResponseModel 
                { 
                    Success = true, 
                    Id = todo.Id,
                    StatusCode = StatusCodes.Status201Created
                } :
                new ResponseModel 
                { 
                    Success = false, 
                    Message = "Unable to create todo",
                    StatusCode = StatusCodes.Status400BadRequest
                };
        }

        public async Task<IResponseModel> UpdateUserAsync(User user)
        {
            var response = await GetUserByIdAsync(user.Id);

            if (!response.Success)
                return new ResponseModel 
                { 
                    Success = false,
                    Message = "User not found",
                    StatusCode = StatusCodes.Status404NotFound
                };

            var userToUpdate = response.Data;

            userToUpdate.Email = user.Email;
            userToUpdate.Username = user.Username;

            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();

            if (user.Settings != null) 
            {
                bool toCreate = false;
                var settignToUpdate = await _context.UserSettings.FirstOrDefaultAsync(e => e.UserId == userToUpdate.Id);
                if (settignToUpdate == null) 
                {
                    toCreate = true;

                    settignToUpdate = new UserSettings();
                    settignToUpdate.UserId = userToUpdate.Id;
                }

                var settings = user.Settings;

                settignToUpdate.DarkTheme = settings.DarkTheme;
                settignToUpdate.NotificationEnabled = settings.NotificationEnabled;
                settignToUpdate.Language = settings.Language;
                settignToUpdate.DarkTheme = settings.DarkTheme;

                if (toCreate) 
                    _context.UserSettings.Add(settignToUpdate);
                else
                    _context.UserSettings.Update(settignToUpdate);
            }

            return await _context.SaveChangesAsync() == 1 ?
                new ResponseModel 
                { 
                    Success = true,
                    StatusCode = StatusCodes.Status200OK
                } :
                new ResponseModel 
                { 
                    Success = false, 
                    Message = "Unable to update user in DB",
                    StatusCode = StatusCodes.Status400BadRequest
                };
        }

        public async Task<IResponseModel> UpdateTodoAsync(Todo todo)
        {
            var response = await GetTodoByIdAsync(todo.Id);

            if (!response.Success)
                return new ResponseModel 
                { 
                    Success = false, 
                    Message = "Todo not found",
                    StatusCode = StatusCodes.Status404NotFound
                };

            var todoToUpdate = response.Data;

            todoToUpdate.Text = todo.Text;
            todoToUpdate.Checked = todo.Checked;
            todoToUpdate.UserId = todo.UserId;

            _context.Todos.Update(todoToUpdate);
            return await _context.SaveChangesAsync() == 1 ?
                new ResponseModel 
                { 
                    Success = true,
                    StatusCode = StatusCodes.Status200OK
                } :
                new ResponseModel 
                {
                    Success = false,
                    Message = "Unable to update todo in DB",
                };
        }

        public async Task<IResponseModel> DeleteUserAsync(int id)
        {
            var userData = await GetUserByIdAsync(id);

            if (!userData.Success)
                return new ResponseModel 
                { 
                    Success = false, 
                    Message = userData.Message,
                    StatusCode = StatusCodes.Status404NotFound
                };

            var todosToDelete = _context.Todos.Where(e => e.UserId == id).Count();
            
            _context.Users.Remove(userData.Data);

            return await _context.SaveChangesAsync() == (1 + todosToDelete) ?
                new ResponseModel 
                {
                    Success = true,
                    StatusCode = StatusCodes.Status204NoContent
                } :
                new ResponseModel 
                { 
                    Success = false, 
                    Message = "Unable to delete user",
                    StatusCode = StatusCodes.Status400BadRequest
                };
        }

        public async Task<IResponseModel> DeleteTodoAsync(int id)
        {
            var todoData = await GetTodoByIdAsync(id);

            if (!todoData.Success)
                return new ResponseModel 
                { 
                    Success = false, 
                    Message = todoData.Message,
                    StatusCode = StatusCodes.Status404NotFound
                };

            _context.Todos.Remove(todoData.Data);
            return await _context.SaveChangesAsync() == 1 ?
                    new ResponseModel 
                    { 
                        Success = true,
                        StatusCode = StatusCodes.Status204NoContent,
                    } :
                    new ResponseModel 
                    {
                        Success = false, 
                        Message = "Unable to delete todo",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

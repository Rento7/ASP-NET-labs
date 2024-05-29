using AspLab4Authorization.Models;

namespace AspLab4Authorization.Data
{
    public interface ITodoistRepository
    {
        Task<IResponseDataModel<IEnumerable<User>>> GetAllUsersAsync();
        Task<IResponseDataModel<User>> GetUserByIdAsync(int id);
        Task<IResponseDataModel<IEnumerable<Todo>>> GetAllTodosAsync();
        Task<IResponseDataModel<IEnumerable<Todo>>> GetAllTodosByUserAsync(int userId);
        Task<IResponseDataModel<Todo>> GetTodoByIdAsync(int id);

        Task<IResponseModel> CreateUserAsync(User user);
        Task<IResponseModel> CreateTodoAsync(Todo todo);

        Task<IResponseModel> UpdateUserAsync(User user);
        Task<IResponseModel> UpdateTodoAsync(Todo todo);


        Task<IResponseModel> DeleteUserAsync(int id);
        Task<IResponseModel> DeleteTodoAsync(int id);
    }
}

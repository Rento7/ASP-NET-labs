using AspLab3MinimalApiEntityFramework.Models;

namespace AspLab3MinimalApiEntityFramework.Data
{
    public interface IReadonlyTodoistRepository
    {
        Task<IResponseDataModel<IEnumerable<User>>> GetAllUsersAsync();
        Task<IResponseDataModel<User>> GetUserByIdAsync(int id);
        Task<IResponseDataModel<IEnumerable<Todo>>> GetAllTodosAsync();
        Task<IResponseDataModel<IEnumerable<Todo>>> GetAllTodosByUserAsync(int userId);
        Task<IResponseDataModel<Todo>> GetTodoByIdAsync(int id);
    }
}

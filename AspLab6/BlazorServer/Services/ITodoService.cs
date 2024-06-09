using BlazorServer.Data;

namespace BlazorServer.Services
{
    public interface ITodoService
    {
        ITodoItem Add(ITodoItem item);
        Task<IEnumerable<ITodoItem>> GetAsync();
        Task<ITodoItem> GetAsync(int id);
        bool Update(int id, ITodoItem item);
        bool Remove(int id);
    }
}

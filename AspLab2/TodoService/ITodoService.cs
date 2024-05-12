namespace TodoService
{
    public interface ITodoService
    {
        bool Add(ITodoItem item);
        Task<IEnumerable<ITodoItem>> GetAsync();
        Task<ITodoItem> GetAsync(long id);
        bool Update(long id, ITodoItem item);
        bool Remove(long id);
    }
}

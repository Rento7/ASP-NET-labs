

namespace TodoService
{
    public class TodoService : ITodoService
    {
        Dictionary<long, ITodoItem> _items;

        public TodoService()
        {
            _items = new Dictionary<long, ITodoItem>()
            {
                {1, new TodoItem() { Id = 1, IsComplete = false, Name = "Clean room" } },
                {2, new TodoItem() { Id = 2, IsComplete = false, Name = "Buy medicine" } },
                {3, new TodoItem() { Id = 3, IsComplete = false, Name = "Create todo api" } },
            };
        }

        public bool Add(ITodoItem item)
        {
            return _items.TryAdd(item.Id, item);
        }

        public async Task<IEnumerable<ITodoItem>> GetAsync()
        {
            return _items.Values;
        }

        public async Task<ITodoItem> GetAsync(long id)
        {
            _items.TryGetValue(id, out ITodoItem item);
            return item;
        }

        public bool Remove(long id)
        {
            return _items.Remove(id);
        }

        public bool Update(long id, ITodoItem item)
        {
            _items.TryGetValue(id, out var todo);

            if (todo == null)
                return false;

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            return true;
        }
    }
}

using BlazorServer.Data;

namespace BlazorServer.Services
{
    public class TodoService : ITodoService
    {
        SortedDictionary<int, ITodoItem> _items;

        public TodoService()
        {
            _items = new SortedDictionary<int, ITodoItem>()
            {
                {1, new TodoItem() { Id = 1, IsComplete = false, Name = "Clean room" } },
                {2, new TodoItem() { Id = 2, IsComplete = false, Name = "Buy medicine" } },
                {3, new TodoItem() { Id = 3, IsComplete = false, Name = "Create todo api" } },
                {4, new TodoItem() { Id = 4, IsComplete = false, Name = "Test Todo 4" } },
                {5, new TodoItem() { Id = 5, IsComplete = false, Name = "Test Todo 5" } },
            };
        }

        public ITodoItem Add(ITodoItem item)
        {
            var lastTodo = _items.Last();

            var todo = new TodoItem() { Id = lastTodo.Key + 1, Name = item.Name, IsComplete = item.IsComplete };

            if (_items.TryAdd(todo.Id, todo))
                return todo;
            else
                return null!;
        }

        public async Task<IEnumerable<ITodoItem>> GetAsync()
        {
            return await Task.FromResult(_items.Values);
        }

        public async Task<ITodoItem> GetAsync(int id)
        {
            _items.TryGetValue(id, out ITodoItem item);
            return await Task.FromResult(item!);
        }

        public bool Remove(int id)
        {
            return _items.Remove(id);
        }

        public bool Update(int id, ITodoItem item)
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
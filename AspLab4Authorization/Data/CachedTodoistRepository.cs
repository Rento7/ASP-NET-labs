using AspLab4Authorization.Models;
using Microsoft.Extensions.Caching.Memory;

namespace AspLab4Authorization.Data
{
    public class CachedTodoistRepository : IReadonlyTodoistRepository, IDisposable
    {
        const string CacheKeyTodos = "Todoist::Todos";
        const string CacheKeyTodosUser = "Todoist::Todos::User";
        const string CacheKeyUsers = "Todoist::Users";
        readonly ITodoistRepository _repository;
        readonly IMemoryCache _cache;
        MemoryCacheEntryOptions _cacheOptions;

        public CachedTodoistRepository(ITodoistRepository repository, IMemoryCache cache) 
        {
            _repository = repository;
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(relative: TimeSpan.FromSeconds(20));
        }

        public async Task<IResponseDataModel<IEnumerable<Todo>>> GetAllTodosAsync()
        {
            return await _cache.GetOrCreateAsync(CacheKeyTodos, async entry =>
            {
                entry.SetOptions(_cacheOptions);
                return await _repository.GetAllTodosAsync();
            });
        }

        public async Task<IResponseDataModel<IEnumerable<Todo>>> GetAllTodosByUserAsync(int userId)
        {
            return await _cache.GetOrCreateAsync(CacheKeyTodosUser + userId, entry =>
            {
                entry.SetOptions(_cacheOptions);
                return _repository.GetAllTodosByUserAsync(userId);
            });
        }

        public async Task<IResponseDataModel<Todo>> GetTodoByIdAsync(int id)
        {
            return await _cache.GetOrCreateAsync(CacheKeyTodos + id, entry =>
            {
                entry.SetOptions(_cacheOptions);
                return _repository.GetTodoByIdAsync(id);
            });
        }

        public async Task<IResponseDataModel<IEnumerable<User>>> GetAllUsersAsync()
        {
            return await _cache.GetOrCreateAsync(CacheKeyUsers, entry =>
            {
                entry.SetOptions(_cacheOptions);
                return _repository.GetAllUsersAsync();
            });
        }

        public async Task<IResponseDataModel<User>> GetUserByIdAsync(int id)
        {
            return await _cache.GetOrCreateAsync(CacheKeyUsers + id, entry =>
            {
                entry.SetOptions(_cacheOptions);
                return _repository.GetUserByIdAsync(id);
            });
        }

        public void Dispose()
        {
            if (_repository is TodoistRepository r)
                r.Dispose();
        }
    }
}

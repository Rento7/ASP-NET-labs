using BlazorServer.Data;

namespace BlazorServer.Pages.Demo
{
    public partial class Todos
    {
        List<ITodoItem> _todos;

        string _newTodoName;
        string _selectedValue;
        ITodoItem _selectedTodo;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var todos = await todoService.GetAsync();
            _todos = todos.ToList();
        }

        string SelectedName 
        {
            get => _selectedTodo == null ? string.Empty : _selectedTodo.Name;
            set 
            {
                if(_selectedTodo == null)
                    return;

                if(_selectedTodo.Name == value)
                    return;

                _selectedTodo.Name = value;
            }
        }

        void TodoAdd() 
        {
            var todo = new TodoItem() { Name = _newTodoName };
            var addedTodo = todoService.Add(todo);

            if (addedTodo != null)
                _todos.Add(addedTodo);
        }

        void TodoRemove()
        {
            if (_selectedTodo == null)
                return;

            var todo = _selectedTodo;

            todoService.Remove(todo.Id);
            _todos.Remove(todo);
        }
    }
}

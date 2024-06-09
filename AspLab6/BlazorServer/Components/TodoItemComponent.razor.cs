using BlazorServer.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorServer.Components
{
    public partial class TodoItemComponent
    {
        [Parameter]
        public ITodoItem Todo { get; set; }


        [Parameter]
        public EventCallback<ITodoItem> Completed { get; set; }

        protected override void OnInitialized()
        {
            if(Todo == null)
                throw new ArgumentNullException(nameof(Todo));
        }

        void OnTodoCompleteChanged(bool completed) 
        {
            Todo.IsComplete = completed;

            if (Completed.HasDelegate) 
                Completed.InvokeAsync(Todo);
        }
    }
}

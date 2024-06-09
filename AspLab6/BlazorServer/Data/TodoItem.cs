namespace BlazorServer.Data
{
    public class TodoItem : ITodoItem
    {
        public int Id { get; init; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}

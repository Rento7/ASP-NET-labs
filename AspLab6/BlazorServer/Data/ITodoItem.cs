namespace BlazorServer.Data
{
    public interface ITodoItem
    {
        public int Id { get; init; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}

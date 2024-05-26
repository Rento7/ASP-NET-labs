namespace AspLab3MinimalApiEntityFramework.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public virtual ICollection<Todo> Todos { get; } = new List<Todo>();
        public UserSettings? Settings { get; set; } = null!;
    }
}

namespace AspLab4Authorization.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public bool Checked { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}

namespace AspLab3MinimalApiEntityFramework.Models
{
    public class TodoDto
    {
        public int Id { get; private set; }
        public string Text { get; set; } = null!;
        public bool Checked { get; set; }
        public int UserId { get; set; }
        public void SetId(int id) => Id = id;
    }
}

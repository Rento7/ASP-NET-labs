namespace AspLab4Authorization.Models
{
    public class UserDto
    {
        public int Id { get; private set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IList<TodoDto> Todos { get; } = new List<TodoDto>();
        public UserSettingsDto Settings { get; set; } = null!;
        public void SetId(int id) => Id = id;
    }
}
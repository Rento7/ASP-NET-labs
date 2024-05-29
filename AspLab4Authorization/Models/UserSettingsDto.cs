namespace AspLab4Authorization.Models
{
    public class UserSettingsDto
    {
        public int Id { get; private set; }
        public string Language { get; set; } = null!;
        public bool DarkTheme { get; set; }
        public bool NotificationEnabled { get; set; }
        public int UserId { get; private set; }

        public void SetId(int id) => Id = id;
        public void SetUserId(int userId) => UserId = userId;
    }
}

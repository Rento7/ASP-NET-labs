namespace AspLab4Authorization.Models
{
    public class UserSettings
    {
        public int Id { get; set; }
        public string Language { get; set; } = null!;
        public bool DarkTheme { get; set; }
        public bool NotificationEnabled { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
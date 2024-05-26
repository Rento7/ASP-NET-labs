using AspLab3MinimalApiEntityFramework.Models;

namespace AspLab3MinimalApiEntityFramework.Data
{
    public class DbInitializer
    {
        public static void Initialize(TodoistContext context) 
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User()
                {
                    Email = "someEmailOne@mail.com",
                    Username = "firstUser",
                },
                new User()
                {
                    Email = "someEmailTwo@mail.com",
                    Username = "secondUser",
                }
            };

            foreach(var user in users)
                context.Users.Add(user);

            context.SaveChanges();

            var usersSettings = new UserSettings[]
            {
                new UserSettings()
                {
                    UserId = 1,
                    DarkTheme = true,
                    NotificationEnabled = true,
                    Language = "en",
                },
                new UserSettings()
                {
                    UserId = 2,
                    DarkTheme = false,
                    NotificationEnabled = false,
                    Language = "ua",
                },
            };

            foreach (var userSetting in usersSettings)
                context.UserSettings.Add(userSetting);

            context.SaveChanges();

            var todos = new Todo[]
            {
                new Todo()
                {
                    UserId = 1,
                    Checked = false,
                    Text = "Clean Room"
                },
                new Todo()
                {
                    UserId = 1,
                    Checked = false,
                    Text = "Feed the dog"
                },
                new Todo()
                {
                    UserId = 1,
                    Checked = true,
                    Text = "Buy medicine"
                },

                new Todo()
                {
                    UserId = 2,
                    Checked = true,
                    Text = "Pay the bills"
                },
                new Todo()
                {
                    UserId = 2,
                    Checked = false,
                    Text = "Repair the sink"
                },
            };

            foreach (var todo in todos)
                context.Todos.Add(todo);

            context.SaveChanges();
        }
    }
}

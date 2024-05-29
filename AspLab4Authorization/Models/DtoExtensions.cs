namespace AspLab4Authorization.Models
{
    public static class DtoExtensions
    {
        public static IEnumerable<UserDto> ToDto(this IEnumerable<User> users) 
        {
            return users.Select(user => user.ToDto());
        }

        public static UserDto ToDto(this User user)
        {
            if (user == null)
                return null!;

            var userDto = new UserDto()
            {
                Username = user.Username,
                Email = user.Email,
                Settings = user.Settings!.ToDto()
            };
            userDto.SetId(user.Id);

            foreach (var todoDto in user.Todos.ToDto())
                userDto.Todos.Add(todoDto);

            return userDto;
        }

        public static UserSettingsDto ToDto(this UserSettings userSettings)
        {
            if (userSettings == null)
                return null!;
            
            var userDto = new UserSettingsDto()
            {
                Language = userSettings.Language,
                DarkTheme = userSettings.DarkTheme,
                NotificationEnabled = userSettings.NotificationEnabled,
            };
            userDto.SetId(userSettings.Id);
            userDto.SetUserId( userSettings.UserId);

            return userDto;
        }

        public static IEnumerable<TodoDto> ToDto(this IEnumerable<Todo> todos)
        {
            return todos.Select(todo => todo.ToDto());
        }

        public static TodoDto ToDto(this Todo todo)
        {
            if (todo == null)
                return null!;

            var todoDto = new TodoDto()
            {
                Text = todo.Text,
                Checked = todo.Checked,
                UserId = todo.UserId
            };
            todoDto.SetId(todo.Id);

            return todoDto;
        }

        public static Todo FromDto(this TodoDto todoDto) 
        {
            if (todoDto == null)
                return null!;

            return new Todo()
            {
                Id = todoDto.Id,
                Text = todoDto.Text,
                Checked = todoDto.Checked,
                UserId = todoDto.UserId
            };
        }

        public static IEnumerable<Todo> FromDto(this IEnumerable<TodoDto> todosDto)
        {
            if (todosDto == null)
                return null!;

            return todosDto.Select(todoDto => todoDto.FromDto());
        }

        public static User FromDto(this UserDto userDto)
        {
            if (userDto == null)
                return null!;


            var user = new User()
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Email = userDto.Email,
                Settings = userDto.Settings.FromDto(),
            };

            return user;
        }

        public static UserSettings FromDto(this UserSettingsDto userSettingsDto)
        {
            if (userSettingsDto == null)
                return null!;

            return new UserSettings()
            {
                Id = userSettingsDto.Id,
                Language = userSettingsDto.Language,
                DarkTheme = userSettingsDto.DarkTheme,
                NotificationEnabled = userSettingsDto.NotificationEnabled,
                UserId = userSettingsDto.UserId
            };
        }
    }
}

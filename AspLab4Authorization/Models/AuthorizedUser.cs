using System.ComponentModel;

namespace AspLab4Authorization.Models
{
    //has nothing to do with User or UserDto
    //need for authorization testing
    public class AuthorizedUser
    {
        [DefaultValue("user")]
        public string Login { get; set; } = null!;

        [DefaultValue("user")]
        public string Password { get; set; } = null!;

        internal bool IsAdmin { get; set; }
    }
}

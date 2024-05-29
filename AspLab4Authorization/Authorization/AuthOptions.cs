using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AspLab4Authorization.Authorization
{
    public class AuthOptions
    {
        public const string UserPolicy = "UserPolicy";
        public const string AdminPolicy = "AdminPolicy";

        public const string UserRole = "User";
        public const string AdminRole = "Admin";

        public const string ISSUER = "MyAuthServer";  
        public const string AUDIENCE = "MyAuthClient";  

        const string KEY = "XMf0pWPjpwvsodXph1EvjUKjdOsvNEiSct4gEnn71TBDVyIBplArwiP0EHv0CsYo8vNjetIPIYDpizIPLwj1UfNQW3Id02s1jR5V"; 
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
             new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}

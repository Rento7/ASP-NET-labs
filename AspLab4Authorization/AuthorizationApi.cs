using AspLab4Authorization.Authorization;
using AspLab4Authorization.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AspLab4Authorization
{
    public static class AuthorizationApi
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user"></param>
        /// <returns>token</returns>
        public static async Task<IResult> Login(AuthorizedUser user)
        {
            var loginData = CreateTestLoginData();

            AuthorizedUser? authorizedUser = loginData.FirstOrDefault(u => u.Login ==
                user.Login && u.Password == user.Password);

            if (authorizedUser is null)
                return Results.Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, authorizedUser.Login) };

            if (authorizedUser.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, AuthOptions.AdminRole));
            else
                claims.Add(new Claim(ClaimTypes.Role, AuthOptions.UserRole));


            var jwt = new JwtSecurityToken (
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials:
                        new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = authorizedUser.Login
            };

            return Results.Json(response);
        }

        static List<AuthorizedUser> CreateTestLoginData()
        {
            return new List<AuthorizedUser>()
            {
                new AuthorizedUser() { Login = "user", Password = "user", IsAdmin = false},
                new AuthorizedUser() { Login = "admin", Password = "admin", IsAdmin = true},
            };
        }
    }
}

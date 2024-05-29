using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AspLab4Authorization.Data;
using AspLab4Authorization.Models;
using AspLab4Authorization.Authorization;
 


namespace AspLab4Authorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddEndpointsApiExplorer();

            // swagger

            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });

            });

            // data and repository
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDbContext<TodoistContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

            builder.Services.AddScoped<ITodoistRepository, TodoistRepository>();
            builder.Services.AddScoped<IReadonlyTodoistRepository, CachedTodoistRepository>();

            //authentication & authorization

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy(AuthOptions.AdminPolicy, policy => policy.RequireRole(AuthOptions.AdminRole))
                .AddPolicy(AuthOptions.UserPolicy, policy => policy.RequireRole(AuthOptions.UserRole));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = AuthOptions.ISSUER,
                     ValidateAudience = true,
                     ValidAudience = AuthOptions.AUDIENCE,
                     ValidateLifetime = true,
                     IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                     ValidateIssuerSigningKey = true,
                 };
             });


            var app = builder.Build();

            //init db
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TodoistContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            RouteGroupBuilder todoGroup = app.MapGroup("/todos");
            todoGroup.MapGet("/", TodoistApi.GetAllTodos)
                .Produces<IResponseDataModel<IEnumerable<TodoDto>>>(200)
                .Produces<IResponseModel>(404);

            todoGroup.MapGet("/{id}", TodoistApi.GetTodoById)
                .Produces<IResponseDataModel<TodoDto>>(200)
                .Produces<IResponseModel>(404);

            todoGroup.MapGet("user/{userId}", TodoistApi.GetAllTodosByUser)
                .Produces<IResponseDataModel<IEnumerable<TodoDto>>>(200)
                .Produces<IResponseModel>(404);

            todoGroup.MapPost("/", TodoistApi.CreateTodo)
                .Produces<IResponseModel>(201)
                .Produces<IResponseModel>(404);

            todoGroup.MapPut("/{id}", TodoistApi.UpdateTodo)
                .Produces<IResponseModel>(200)
                .Produces<IResponseModel>(400)
                .Produces<IResponseModel>(404);

            todoGroup.MapDelete("/{id}", TodoistApi.DeleteTodo)
                .Produces<IResponseModel>(204)
                .Produces<IResponseModel>(400)
                .Produces<IResponseModel>(404);


            RouteGroupBuilder userGroup = app.MapGroup("/users");
            userGroup.MapGet("/", TodoistApi.GetAllUsers)
                .Produces<IResponseDataModel<IEnumerable<UserDto>>>(200)
                .Produces<IResponseModel>(404);

            userGroup.MapGet("/{id}", TodoistApi.GetUserById)
                .Produces<IResponseDataModel<UserDto>>(200)
                .Produces<IResponseModel>(404);

            userGroup.MapPost("/", TodoistApi.CreateUser)
                .Produces<IResponseModel>(201)
                .Produces<IResponseModel>(404);

            userGroup.MapPut("/{id}", TodoistApi.UpdateUser)
                .Produces<IResponseModel>()
                .Produces<IResponseModel>(400)
                .Produces<IResponseModel>(404);

            userGroup.MapDelete("/{id}", TodoistApi.DeleteUser)
                .Produces<IResponseModel>(204)
                .Produces<IResponseModel>(400)
                .Produces<IResponseModel>(404);

            app.MapPost("/login/", AuthorizationApi.Login);

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}

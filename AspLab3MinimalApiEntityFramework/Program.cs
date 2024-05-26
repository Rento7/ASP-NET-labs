using AspLab3MinimalApiEntityFramework.Data;
using AspLab3MinimalApiEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace AspLab3MinimalApiEntityFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDbContext<TodoistContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

            builder.Services.AddScoped<ITodoistRepository, TodoistRepository>();
            builder.Services.AddScoped<IReadonlyTodoistRepository, CachedTodoistRepository>();

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

            app.Run();
        }
    }
}

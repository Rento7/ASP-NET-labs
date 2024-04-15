using AspLab1.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AspLab1
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages()
                .AddViewOptions(options =>
                {
                    options.HtmlHelperOptions.ClientValidationEnabled = true;
                });

            builder.Services.AddSingleton<IDemoService, DemoService>();
            builder.Services.AddSingleton<ICarService, CarService>();
            builder.Services.AddSingleton<IValidationService, ValidationService>();
            
            IExceptionService _exceptionService = new ExceptionService();
            builder.Services.AddSingleton<IExceptionService>(_exceptionService);
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                _exceptionService.Add(eventArgs.Exception);
            };

            
            var app = builder.Build();

            //if (!app.Environment.IsDevelopment())
            //{
                app.UseExceptionHandler("/Exception");
            //}

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.Use(async (context, next) => {
            //    context.Request.EnableBuffering();
            //    await next();
            //});

            app.Run();
        }
    }
}
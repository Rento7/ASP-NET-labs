using AspLab1.Extensions;
using AspLab1.Services;
using AspLab1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.AccessControl;

namespace AspLab1.Controllers
{
    public class DemoController : Controller
    {
        IDemoService _demoService;
        IValidationService _validationService;

        public DemoController(IDemoService demoService, IValidationService validationService) 
        {
            _demoService = demoService;
            _validationService = validationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DemoViewModel demoViewModel)
        {
            if (!_validationService.Validate(demoViewModel, out var validationErros))
            {
                ViewBag.ValidationErrors = validationErros;
            }
            else
            {
                _demoService.AddItem(demoViewModel.Model);
            }

            return Redirect(nameof(Index));
        }


        public void ThrowRandomException()
        {
            var exceptionTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
                a.GetTypes().Where(t =>
                    t.IsSubclassOf(typeof(Exception)) &&
                    t.GetConstructors().Where(c =>
                        c.GetParameters().Length == 1 &&
                        c.GetParameters().Where(p => p.Position == 0 && p.Name == "message" && p.ParameterType == typeof(string))
                        .FirstOrDefault() != null)
                    .FirstOrDefault() != null)).ToArray();

            int index = new Random().Next(0, exceptionTypes.Length);
            Type exceptionType = exceptionTypes[index];
            Exception exception = (Exception)Activator.CreateInstance(exceptionType, $"Message for random exception. Index in exception list: {index}");

            throw exception;
        }
    }
}

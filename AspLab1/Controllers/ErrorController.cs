using AspLab1.Services;
using AspLab1.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspLab1.Controllers
{
    public class ErrorController : Controller
    {
        IExceptionService _exceptionService;
        public ErrorController(IExceptionService exceptionService)
        {
            _exceptionService = exceptionService;
        }

        [Route("/Exception")]
        public IActionResult Exception()
        {
            var exception = _exceptionService.LastException;

            if (exception == null)
                return new EmptyResult();

            return View(new ExceptionViewModel(exception));
        }

        [Route("/Error/{code}")]
        public IActionResult Error(int code)
        {
            return View(new ErrorViewModel(code));
        }
    }
}

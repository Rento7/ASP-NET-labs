using AspLab1.Services;
using AspLab1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspLab1.Controllers
{
    public class CarsController : Controller
    {
        ICarService _carService;
        IValidationService _validationService;

        string[] _validationErros;

        public CarsController(ICarService carService , IValidationService validationService)
        {
            _carService = carService;
            _validationService = validationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarViewModel viewModel)
        {
            if(!_validationService.Validate(viewModel, out var validationErrors))
            {
                TempData["ValidationErrors"] = validationErrors;
                _validationErros = validationErrors;
            }
            else 
            {
                _carService.AddCar(viewModel.Car);
            }

            return Redirect(nameof(Index));
        }
    }
}

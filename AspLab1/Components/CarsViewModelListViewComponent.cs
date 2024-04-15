using Microsoft.AspNetCore.Mvc;
using AspLab1.ViewModels;
using AspLab1.Services;

namespace AspLab1.Components
{
    public class CarsViewModelListViewComponent : ViewComponent
    {
        ICarService _carService;

        public CarsViewModelListViewComponent(ICarService carService)
        {
            _carService = carService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModels = _carService.GetCars()?.Select(item => new CarViewModel(item));
            return View(viewModels);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using AspLab1.ViewModels;
using AspLab1.Services;

namespace AspLab1.Components
{
    public class DemoViewModelListViewComponent : ViewComponent
    {
        IDemoService _demoService;

        public DemoViewModelListViewComponent(IDemoService demoService) 
        { 
            _demoService = demoService;
        }

        public IViewComponentResult Invoke() 
        {
            var viewModels = _demoService.GetItems()?.Select(item => new DemoViewModel() { Model = item });
            return View(viewModels);
        }
    }
}

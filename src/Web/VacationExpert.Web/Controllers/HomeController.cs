namespace VacationExpert.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Services.Data.PropertyServices;
    using VacationExpert.Web.ViewModels;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public class HomeController : BaseController
    {
        private readonly IPropertyService propertyService;

        public HomeController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        public IActionResult Index()
        {
            var result = this.propertyService.GetLastFIve();
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

namespace VacationExpert.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Services.Data.PropertyServices;

    public class PropertyController : Controller
    {
        private readonly IPropertyService propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        [HttpGet]
        public IActionResult DisplayProperty(string id)
        {
            var result = this.propertyService.GetProperty(id);
            return this.View(result);
        }
    }
}

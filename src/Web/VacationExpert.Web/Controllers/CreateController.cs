namespace VacationExpert.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using VacationExpert.Services.Data.PropertyServices;
    using VacationExpert.Services.Models;

    public class CreateController : Controller
    {
        private readonly IPropertyService propertyService;

        public CreateController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public Task<IActionResult> Index(CreatePropertyInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.Redirect("/");
        }
    }
}

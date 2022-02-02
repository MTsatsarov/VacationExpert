namespace VacationExpert.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Services.Data.PropertyServices;
    using VacationExpert.Services.Models;

    public class CreateController : Controller
    {
        private readonly IPropertyService propertyService;

        public CreateController(IPropertyService propertyService) => this.propertyService = propertyService;

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [RequestSizeLimit(120 * 1024 * 1024)]
        public async Task<IActionResult> Index(CreatePropertyInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.Images.Count > 10)
            {
                this.ModelState.AddModelError("images", "You cannot upload more than 10 images");
                return this.View(model);
            }

            if (model.Images.Any(x => x.Length > 10 * 1024 * 1024))
            {
                this.ModelState.AddModelError("images", "Image cannot be more than 10mb");
                return this.View(model);
            }

            await this.propertyService.Create(model);

            return this.Redirect("/");
        }
    }
}

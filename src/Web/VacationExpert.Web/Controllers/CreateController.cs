namespace VacationExpert.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Common;
    using VacationExpert.Data.Models;
    using VacationExpert.Services.Data.PropertyServices;
    using VacationExpert.Services.Models;

    [Authorize(Roles = GlobalConstants.CreatorRoleName)]
    public class CreateController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public CreateController(IPropertyService propertyService, UserManager<ApplicationUser> userManager)
        {
            this.propertyService = propertyService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [RequestSizeLimit(120 * 1024 * 1024)]
  
        public async Task<IActionResult> Index(CreatePropertyInputModel model)
        {
            model.UserId = this.userManager.GetUserId(this.User);

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

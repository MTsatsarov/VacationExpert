namespace VacationExpert.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Services.Models;

    public class CreateController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Index(CreatePropertyInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.Redirect("/");
        }
    }
}

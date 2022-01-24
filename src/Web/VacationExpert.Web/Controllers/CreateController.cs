namespace VacationExpert.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CreateController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace VacationExpert.Web.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public IActionResult GetData()
        {
            return View();
        }
    }
}

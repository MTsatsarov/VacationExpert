namespace VacationExpert.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Common;
    using VacationExpert.Services.Data.PropertyServices;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

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

        [Route("Property/Delete/id")]
        public async Task<IActionResult> Delete(string owner, string id)
        {
            await this.propertyService.Delete(owner, id);
            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.CreatorRoleName)]
        [Route("Property/MyProperties/page")]
        public IActionResult MyProperties(int id)
        {
            var properties = new PropertyListViewModel();
            try
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                properties = this.propertyService.GetByUser(userId, id);
            }
            catch (System.Exception ex)
            {
                return this.View("Error", new ErrorViewModel()
                {
                    RequestId = ex.Message,
                });
            }

            return this.View(properties);
        }
    }
}

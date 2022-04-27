namespace VacationExpert.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Common;
    using VacationExpert.Services.Data.PropertyService.Services;
    using VacationExpert.Services.Data.PropertyServices;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public class PropertyController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IPropertyServices services;

        public PropertyController(IPropertyService propertyService, IPropertyServices services)
        {
            this.propertyService = propertyService;
            this.services = services;
        }

        [HttpGet]
        public IActionResult DisplayProperty(string id)
        {
            var result = this.propertyService.GetProperty(id);
            return this.View(result);
        }

        [Route("Property/Delete/id")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.propertyService.Delete(userId, id);
            }
            catch (System.Exception e)
            {
                return this.View("Error", e.Message);
            }

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.CreatorRoleName)]
        [Route("Property/edit/id")]
        public IActionResult Edit(string id)
        {
            var user = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model = this.propertyService.GetUpdateModel(id);
            this.ViewData["Property"] = model;
            this.ViewData["Services"] = this.services.GetServices();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.CreatorRoleName)]
        [Route("Property/edit/id")]
        public async Task<IActionResult> Edit(CreatePropertyInputModel model)
        {
            var user = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.UserId = user;
            try
            {
                await this.propertyService.Update(model);
            }
            catch (System.Exception)
            {
                return this.View("Error");
            }

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

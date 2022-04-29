namespace VacationExpert.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Common;
    using VacationExpert.Services.Data.ImageService;

    public class ImageController : Controller
    {
        private readonly IImageService service;

        public ImageController(IImageService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> GetThumbnail([FromRoute] string id)
        {
            var result = await this.service.GetImageData(id, GlobalConstants.ThumbnailContent);
            return this.File(result, "image/jpeg");
        }

        [HttpPost]
        [Route("image/delete")]
        [IgnoreAntiforgeryToken]
        [Authorize(Roles =GlobalConstants.CreatorRoleName)]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                await this.service.Delete(id, userId);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }

            return this.Ok();
        }

        public async Task<IActionResult> GetFullscreen([FromRoute] string id)
        {
            var result = await this.service.GetImageData(id, GlobalConstants.FullscreenContent);
            return this.File(result, "image/jpeg");
        }
    }
}

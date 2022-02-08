namespace VacationExpert.Web.Controllers
{
    using System.Threading.Tasks;

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

        public async Task<IActionResult> GetFullscreen([FromRoute] string id)
        {
            var result = await this.service.GetImageData(id, GlobalConstants.FullscreenContent);
            return this.File(result, "image/jpeg");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VacationExpert.Services.Data.ImageService;

namespace VacationExpert.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService service;

        public ImageController(IImageService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> GetThumbnail([FromRoute] string id)
        {
            var result = await this.service.GetImageData(id);
            return this.File(result, "image/jpeg");
        }
    }
}

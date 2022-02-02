namespace VacationExpert.Services.Data.ImageService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VacationExpert.Data.Models;
    using VacationExpert.Services.Models;

    public interface IImageService
    {

        public Task<List<Image>> ImageProcess(IEnumerable<ImageInputModel> images);
    }
}

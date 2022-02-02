namespace VacationExpert.Services.Data.ImageService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;
    using VacationExpert.Services.Models;

    public class ImageService : IImageService
    {
        private const int ThumbnailWidth = 300;
        private const int FullScreenWidth = 1200;

        public async Task<List<VacationExpert.Data.Models.Image>> ImageProcess(IEnumerable<ImageInputModel> images)
        {
            var tasks = new List<Task>();
            var inputImages = new List<VacationExpert.Data.Models.Image>();
            foreach (var image in images)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        using var imageResult = await Image.LoadAsync(image.Content);
                        var original = await SaveImage(imageResult, imageResult.Width);
                        var fullscreen = await SaveImage(imageResult, FullScreenWidth);
                        var thumbnail = await SaveImage(imageResult, ThumbnailWidth);
                        inputImages.Add(new VacationExpert.Data.Models.Image
                        {
                            OriginalFileName = image.Name,
                            OriginalType = image.Type,
                            OriginalContent = original,
                            FullScreenContent = fullscreen,
                            ThumbnailContent = thumbnail,
                        });
                    }
                    catch (Exception)
                    {
                        throw new InvalidOperationException("Please upload only images");
                    }
                }));
                await Task.WhenAll(tasks);

            }

            return inputImages;
        }

        private static async Task<byte[]> SaveImage(Image image, int resizeWIdth)
        {
            var width = image.Width;
            var heigh = image.Height;

            if (width > resizeWIdth)
            {
                heigh = (int)((double)resizeWIdth / width * heigh);
                width = resizeWIdth;
            }

            image.Mutate(i => i
            .Resize(new Size(width, heigh)));

            image.Metadata.ExifProfile = null;

            var memoryStream = new MemoryStream();
            return memoryStream.ToArray();
        }
    }
}

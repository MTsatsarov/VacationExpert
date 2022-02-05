namespace VacationExpert.Services.Data.ImageService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;
    using VacationExpert.Data;
    using VacationExpert.Services.Models;

    public class ImageService : IImageService
    {
        private const int ThumbnailWidth = 300;
        private const int FullScreenWidth = 1200;

        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ApplicationDbContext dbContext;

        public ImageService(IServiceScopeFactory serviceScopeFactory, ApplicationDbContext dbContext)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.dbContext = dbContext;
        }

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

        public async Task<Stream> GetImageData(string propertyId)
        {

            var data = this.dbContext.Database;
            var dbConnection = (SqlConnection)data.GetDbConnection();
            var command = new SqlCommand(
                $"SELECT ThumbnailContent FROM Images WHERE PropertyId = @propertyId",
                dbConnection);

            command.Parameters.Add(new SqlParameter("@propertyId", propertyId));
            dbConnection.Open();

            var reader = await command.ExecuteReaderAsync();
            Stream result = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = reader.GetStream(0);
                }
            }

            await reader.CloseAsync();
            return result;
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

            await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
            {
                Quality = 75,
            });
            return memoryStream.ToArray();
        }

       
    }
}

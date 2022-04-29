namespace VacationExpert.Services.Data.ImageService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;
    using VacationExpert.Data;
    using VacationExpert.Services.Models;

    public class ImageService : IImageService
    {
        private const int ThumbnailWidth = 300;
        private const int FullScreenWidth = 1200;

        private readonly ApplicationDbContext dbContext;

        public ImageService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<List<VacationExpert.Data.Models.Image>> ImageProcess(IEnumerable<ImageInputModel> images)
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

        public async Task<Stream> GetImageData(string id, string content)
        {
            var data = this.dbContext.Database;
            var dbConnection = (SqlConnection)data.GetDbConnection();
            var command = new SqlCommand(
                $"SELECT {content} FROM Images WHERE Id = @id",
                dbConnection);

            command.Parameters.Add(new SqlParameter("@id", id));
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
       
        public List<string> GetAllImages(string propertyId)
        {
            return this.dbContext.Images.Where(x => x.PropertyId == propertyId).Select(x => x.Id.ToString()).ToList();
        }

        public async Task Delete(string id, string userId)
        {
            var image = this.dbContext.Images.FirstOrDefault(x => x.Id.ToString() == id);
            if (image == null)
            {
                throw new InvalidOperationException("Image not found");
            }

            if (image.Property.UserId != userId)
            {
                throw new InvalidOperationException("You are not allowed to delete images");
            }

            image.IsDeleted = true;
            this.dbContext.Images.Update(image);
            await this.dbContext.SaveChangesAsync();
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

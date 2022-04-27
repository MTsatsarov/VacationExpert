namespace VacationExpert.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;
    using VacationExpert.Data;
    using VacationExpert.Data.Models;
    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Services.Data.ImageService;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public class SearchServiceTests
    {
        [Test]
        public void Assert_PageIsLessThatOne_ThrowError()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("THrowErrorIfPageInvald");

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<IImageService>();
            var service = new SearchService.SearchService(db, imageService.Object);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => service.GetResults(new Models.SearchInputModel(), 0));
            Assert.That(ex.Message, Is.EqualTo("Invalid page"));
        }

        [Test]
        public async Task Return_EmptyObject_IfNoResults()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("EmptyResult");

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<IImageService>();
            var service = new SearchService.SearchService(db, imageService.Object);
            var result = await service.GetResults(new Models.SearchInputModel() { City = "Burgas" }, 1);

            Assert.That(result.Properties.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task Assert_GetResult_ReturnProperResults()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("CorrectResult");

            var db = new ApplicationDbContext(optionsBuilder.Options);
            db.Properties.Add(new Property()
            {
                Address = new Address() { City = Enum.Parse<City>("Burgas") },
                Contact = new Contact(),
                Description = "Test Description",
                Facility = new Facility(),
                Images = new HashSet<Image>() { new Image() { Id = Guid.NewGuid() } },
                Name = "Test_name",
                Rating = 4,
                Reviews = new HashSet<Review>(),
                Rooms = new HashSet<Room>(),
            });
            await db.SaveChangesAsync();
            var imageService = new Mock<IImageService>();
            var service = new SearchService.SearchService(db, imageService.Object);
            var result = await service.GetResults(new Models.SearchInputModel() { City = "Burgas" }, 1);

            Assert.That(result.Properties.Count, Is.EqualTo(1));
            Assert.That(result.Properties.First().City, Is.EqualTo("Burgas"));
        }
    }
}

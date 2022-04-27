namespace VacationExpert.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;
    using VacationExpert.Data;
    using VacationExpert.Data.Models;
    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Services.Data.ImageService;
    using VacationExpert.Services.Data.ReviewServices;
    using VacationExpert.Services.Models;

    public class PropertyServiceTests
    {
        private CreatePropertyInputModel inputModel;

        private Property propertyEntity;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.propertyEntity = new Property()
            {
                Address = new Address() { City = Enum.Parse<City>("Burgas") },
                Contact = new Contact(),
                Description = "Test Description",
                Facility = new Facility(),
                Images = new HashSet<Image>() { new Image() { Id = Guid.NewGuid() } },
                Name = "Test_name",
                Rating = 4,
                Reviews = new HashSet<Review>() { new Review() },
                Rooms = new HashSet<Room>() { new Room() { Beds = new HashSet<Bed>() } },
                UserId = Guid.NewGuid().ToString(),
            };

            this.inputModel = new CreatePropertyInputModel()
            {
                Address = new AddressInputModel()
                {
                    City = "Burgas",
                    Country = "Bulgaria",
                    StreetAddress = "test_address",
                    ZipCode = "8000",
                },
                Contact = new ContactInputModel()
                {
                    AdditionalPhone = "+33333333333",
                    ContactName = "test_contact",
                    Phone = "5555555555555",
                },
                Description = "test_description",
                Name = "test_property",
                Rating = 4,
                UserId = Guid.NewGuid().ToString(),
                Facilities = new FacilityInputModel()
                {
                    Breakfast = "No",
                    Language = "English",
                    Parking = "No",
                    Services = new List<ServicesInputModel>()
                    {
                        new ServicesInputModel()
                        {
                            Name = "swimmingPool",
                            Selected = true,
                        },
                    },
                },
                Rooms = new List<RoomInputModel>()
                {
                    new RoomInputModel()
                    {
                        GuestsCount = 1,
                        RoomCount = 1,
                        RoomSize = 1,
                        Type = "Single",
                        SmokingPolicy = "Smoking",
                        Beds = new List<BedInputModel>()
                        {
                            new BedInputModel()
                            {
                                Count = 1,
                                Type = "FullBeds131150cmwide",
                            },
                        },
                    },
                },
                Images = new FormFileCollection()
                {
                },
            };
        }

        [Test]
        public async Task SuccesfullyCreateProperty()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
 .UseSqlServer("SuccesfullyCreateProperty");

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<VacationExpert.Services.Data.ImageService.ImageService>(db);
            imageService.Setup(x => x.ImageProcess(It.IsAny<List<ImageInputModel>>())).ReturnsAsync(new List<Image>());
            var reviewSerivce = new ReviewService(db);
            var propertyService = new VacationExpert.Services.Data.PropertyServices.PropertyService(db, imageService.Object, reviewSerivce);

            await propertyService.Create(this.inputModel);

            Assert.That(db.Properties.Count, Is.EqualTo(1));
        }

        [Test]
        [TestCase("Invalid_id")]
        [TestCase(null)]
        public void AssertErrorIfUserInvalid(string userId)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
 .UseInMemoryDatabase("Server=DESKTOP-K5IJHSE\\SQLEXPRESS;Database=VacationExpert;Trusted_Connection=True;MultipleActiveResultSets=true");

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<VacationExpert.Services.Data.ImageService.ImageService>(db);
            imageService.Setup(x => x.ImageProcess(It.IsAny<List<ImageInputModel>>())).ReturnsAsync(new List<Image>());
            var reviewSerivce = new ReviewService(db);
            var propertyService = new VacationExpert.Services.Data.PropertyServices.PropertyService(db, imageService.Object, reviewSerivce);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => propertyService.Delete(userId, this.propertyEntity.Id));

            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }

        [Test]
        [TestCase("Invalidid", "invalidPropertyIdDb2")]
        [TestCase(null, "invalidPropertyIdDb3")]
        public async Task AssertThrowsError_IFPropertyNotFound(string propertyId, string dbName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase(dbName);

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<VacationExpert.Services.Data.ImageService.ImageService>(db);
            imageService.Setup(x => x.ImageProcess(It.IsAny<List<ImageInputModel>>())).ReturnsAsync(new List<Image>());
            var reviewSerivce = new ReviewService(db);
            var propertyService = new VacationExpert.Services.Data.PropertyServices.PropertyService(db, imageService.Object, reviewSerivce);

            await db.Properties.AddAsync(this.propertyEntity);
            await db.SaveChangesAsync();

            var userId = this.propertyEntity.UserId;
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => propertyService.Delete(userId, propertyId));

            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo("Property not found"));
        }

        [Test]
        public async Task Assert_SuccesfullyDeleteProperty()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("SuccesfullyDeleteProperty");

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<VacationExpert.Services.Data.ImageService.ImageService>(db);
            imageService.Setup(x => x.ImageProcess(It.IsAny<List<ImageInputModel>>())).ReturnsAsync(new List<Image>());
            var reviewSerivce = new ReviewService(db);
            var propertyService = new VacationExpert.Services.Data.PropertyServices.PropertyService(db, imageService.Object, reviewSerivce);

            await db.Properties.AddAsync(this.propertyEntity);
            await db.SaveChangesAsync();

            Assert.That(db.Properties.Count, Is.EqualTo(1));
            var userId = this.propertyEntity.UserId;

            await propertyService.Delete(userId, this.propertyEntity.Id);

            Assert.That(db.Properties.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task Assert_GetByIdReturnProperProperty()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("PropertySuccesfullyReturnedById");

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<VacationExpert.Services.Data.ImageService.ImageService>(db);
            imageService.Setup(x => x.ImageProcess(It.IsAny<List<ImageInputModel>>())).ReturnsAsync(new List<Image>());
            var reviewSerivce = new ReviewService(db);
            var propertyService = new VacationExpert.Services.Data.PropertyServices.PropertyService(db, imageService.Object, reviewSerivce);

            await db.Properties.AddAsync(this.propertyEntity);
            await db.SaveChangesAsync();

            var userId = this.propertyEntity.UserId;

            var property = propertyService.GetProperty(this.propertyEntity.Id);

            Assert.That(property.Id == this.propertyEntity.Id);
            Assert.That(property.Name == this.propertyEntity.Name);
        }

        [Test]
        [TestCase(0, "Invalid1")]
        [TestCase(-4, "invalidTwoo")]
        [TestCase(555, "invalidThree")]
        public async Task Assert_ThrowErrorIfPageInvalid_GetByUser(int page, string dbName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase(dbName);

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<VacationExpert.Services.Data.ImageService.ImageService>(db);
            imageService.Setup(x => x.ImageProcess(It.IsAny<List<ImageInputModel>>())).ReturnsAsync(new List<Image>());
            var reviewSerivce = new ReviewService(db);
            var propertyService = new VacationExpert.Services.Data.PropertyServices.PropertyService(db, imageService.Object, reviewSerivce);

            await db.Properties.AddAsync(this.propertyEntity);
            await db.SaveChangesAsync();

            var userId = this.propertyEntity.UserId;

            var ex = Assert.Throws<InvalidOperationException>(() => propertyService.GetByUser(userId, page));

            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo("Invalid page"));
        }

        [Test]
        public void IfNoPropertyies_ReturnEmptyList()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("No propertyies foundd");

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<VacationExpert.Services.Data.ImageService.ImageService>(db);
            imageService.Setup(x => x.ImageProcess(It.IsAny<List<ImageInputModel>>())).ReturnsAsync(new List<Image>());
            var reviewSerivce = new ReviewService(db);
            var propertyService = new VacationExpert.Services.Data.PropertyServices.PropertyService(db, imageService.Object, reviewSerivce);

            var userId = this.propertyEntity.UserId;

            Assert.That(propertyService.GetByUser(userId).Properties.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Assert_GetByUser_ReturnProperResult()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
 .UseInMemoryDatabase("SuccesfullyReturnUserPropertiessTest1");

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var imageService = new Mock<VacationExpert.Services.Data.ImageService.ImageService>(db);
            imageService.Setup(x => x.ImageProcess(It.IsAny<List<ImageInputModel>>())).ReturnsAsync(new List<Image>());
            var reviewSerivce = new ReviewService(db);
            var propertyService = new VacationExpert.Services.Data.PropertyServices.PropertyService(db, imageService.Object, reviewSerivce);

            var propertyModel = new Property()
            {
                Address = new Address() { City = Enum.Parse<City>("Burgas") },
                Contact = new Contact(),
                Description = "Test Description",
                Facility = new Facility(),
                Images = new HashSet<Image>() { new Image() { Id = Guid.NewGuid() } },
                Name = "Test_name",
                Rating = 4,
                Reviews = new HashSet<Review>() { new Review() },
                Rooms = new HashSet<Room>() { new Room() { Beds = new HashSet<Bed>() } },
                UserId = Guid.NewGuid().ToString(),
            };

            await db.Properties.AddAsync(propertyModel);
            await db.SaveChangesAsync();
            var userId = propertyModel.UserId;

            this.propertyEntity.Name = "NewName";
            this.propertyEntity.UserId = Guid.NewGuid().ToString();

            await db.Properties.AddAsync(this.propertyEntity);
            await db.SaveChangesAsync();

            var property = propertyService.GetByUser(userId);

            Assert.That(db.Properties.Count(), Is.EqualTo(2));
            Assert.That(property.Properties.Count(), Is.EqualTo(1));
            Assert.That(property.Properties.First().Name, Is.EqualTo(propertyModel.Name));
        }
    }
}
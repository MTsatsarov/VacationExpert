namespace VacationExpert.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using VacationExpert.Data;
    using VacationExpert.Data.Models;
    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Services.Data.ReviewServices;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.ReviewViewModels;

    public class ReviewServiceTests
    {
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
                Reviews = new HashSet<Review>(),
                Rooms = new HashSet<Room>(),
                UserId = Guid.NewGuid().ToString(),
            };
        }

        [Test]
        public void AssertThrows_InvalidPropertyId()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("THrowErrorIfPropertyIdInvald");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var reviewService = new ReviewService(db);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => reviewService.AddReview(new ReviewInputModel()));

            Assert.That(ex.Message, Is.EqualTo("Property not found"));
        }

        [Test]
        public void AssertThrows_IfModelIsInvalid()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("THrowErrorIfModelInvald");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var reviewService = new ReviewService(db);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => reviewService.AddReview(null));

            Assert.That(ex.Message, Is.EqualTo("Review is invalid"));
        }

        [Test]
        public async Task AssertSuccesfully_AddReview()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("SuccesfullyAddReview3");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            await db.Properties.AddAsync(this.propertyEntity);
            await db.SaveChangesAsync();
            var reviewService = new ReviewService(db);

            Assert.That(db.Reviews.Count, Is.EqualTo(0));

            await reviewService.AddReview(new ReviewInputModel()
            {
                Content = "Test_Content",
                PropertyId = this.propertyEntity.Id,
                Rating = 4,
            });
            await reviewService.AddReview(new ReviewInputModel()
            {
                Content = "Test_Content2",
                PropertyId = this.propertyEntity.Id,
                Rating = 5,
            });

            Assert.That(reviewService.Count(this.propertyEntity.Id), Is.EqualTo(2));
        }

        [Test]
        public async Task AssertThrows_IfPageINvalid()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("ThrowIfInvalidPageForReview");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var reviewService = new ReviewService(db);

            var ex = Assert.Throws<InvalidOperationException>(() => reviewService.GetReviews(this.propertyEntity.Id, "0"));
        }

        [Test]
        public void AssertThrows_IfPropertyINvalid()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("ThrowIfInvalidPropertyForReview");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var reviewService = new ReviewService(db);

            var ex = Assert.Throws<InvalidOperationException>(() => reviewService.GetReviews("test_test", "1"));
        }

        [Test]
        public async Task Assert_GetReviews_ReturnProperReviews()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
  .UseInMemoryDatabase("ReturnProperReviews");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var reviewService = new ReviewService(db);

            await db.Properties.AddAsync(this.propertyEntity);
            await db.SaveChangesAsync();

            var property = new Property()
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
                UserId = Guid.NewGuid().ToString(),
            };
            await db.Properties.AddAsync(property);

            await db.SaveChangesAsync();

            var review = new ReviewInputModel()
            {
                Content = "Test_Content",
                PropertyId = this.propertyEntity.Id,
                Rating = 4,
            };
            await reviewService.AddReview(review);

            await reviewService.AddReview(new ReviewInputModel()
            {
                Content = "Test_Content",
                PropertyId = property.Id,
                Rating = 4,
            });
            var a = db.Properties.ToList();
            var b = db.Reviews.ToList();

            var reviews = reviewService.GetReviews(property.Id);
            Assert.That(reviews, Is.Not.Null);
            Assert.That(reviews, Is.InstanceOf<ReviewListViewModel>());
            Assert.That(reviews.Reviews.Count, Is.EqualTo(1));
            Assert.That(reviews.Reviews.First().Content, Is.EqualTo(review.Content));
        }
    }
}

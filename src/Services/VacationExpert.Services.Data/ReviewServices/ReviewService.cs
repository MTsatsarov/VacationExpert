namespace VacationExpert.Services.Data.ReviewServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using VacationExpert.Data;
    using VacationExpert.Data.Models;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.ReviewViewModels;

    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext db;

        public ReviewService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddReview(ReviewInputModel model)
        {
            var review = new Review()
            {
                Content = model.Content,
                Rating = model.Rating,
                UserId = model.UserId,
                PropertyId = model.PropertyId,
            };

            await this.db.Reviews.AddAsync(review);
            await this.db.SaveChangesAsync();
        }

        public ReviewListViewModel GetReviews(string propertyId)
        {
            var model = new ReviewListViewModel();
            var reviews = this.db.Reviews.Where(x => x.PropertyId == propertyId).Select(x => new ReviewInListViewModel
            {
                Content = x.Content,
                Username = x.User.UserName,
                Rating = x.Rating,
                DateTime= x.CreatedOn.ToShortDateString(),
            }).ToList().Take(3);

            model.Reviews.AddRange(reviews);
            return model;
        }
    }
}

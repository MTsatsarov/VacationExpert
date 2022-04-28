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
        private const int ReviewsPerPage = 3;
        private readonly ApplicationDbContext db;

        public ReviewService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddReview(ReviewInputModel model)
        {
            if (model == null)
            {
                throw new InvalidOperationException("Review is invalid");
            }

            if (!this.db.Properties.Any(x => x.Id == model.PropertyId))
            {
                throw new InvalidOperationException("Property not found");
            }

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

        public int Count(string propertyId)
        {
            return this.db.Reviews.Where(x => x.PropertyId == propertyId).Count();
        }

        public ReviewListViewModel GetReviews(string propertyId, string page = "1")
        {
            var currentPage = int.Parse(page);

            if (currentPage <= 0)
            {
                throw new InvalidOperationException("Invalid page");
            }

            if (!this.db.Properties.Any(x => x.Id == propertyId))
            {
                throw new InvalidOperationException("Property not found");
            }

            var model = new ReviewListViewModel();
            model.TotalPages = (int)Math.Ceiling((double)this.Count(propertyId) / (double)ReviewsPerPage);

            model.Reviews = this.db.Reviews.Where(x => x.PropertyId == propertyId).Select(x => new ReviewInListViewModel
            {
                Id = x.Id,
                LikeCount = x.Votes.Where(x => x.Like == true).Count(),
                DislikeCount = x.Votes.Where(x => x.Like == false).Count(),
                Content = x.Content,
                DateTime = x.CreatedOn.ToShortDateString(),
                Rating = x.Rating,
                Username = x.User.UserName,
            }).Skip((currentPage - 1) * ReviewsPerPage).Take(3).ToList();
            model.CurrentPage = currentPage;

            return model;
        }
    }
}

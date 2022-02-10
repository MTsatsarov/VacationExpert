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
            var model = new ReviewListViewModel();
            var currentPage = int.Parse(page);
            model.TotalPages = (int)Math.Ceiling((double)this.Count(propertyId) / (double)ReviewsPerPage);

            if (currentPage <= 0 || currentPage > model.TotalPages)
            {
                throw new InvalidOperationException("Invalid page");
            }

            model.Reviews = this.db.Reviews.Where(x => x.PropertyId == propertyId).Select(x => new ReviewInListViewModel
            {
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

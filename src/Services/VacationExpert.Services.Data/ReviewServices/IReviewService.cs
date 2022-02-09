namespace VacationExpert.Services.Data.ReviewServices
{
    using System.Threading.Tasks;

    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.ReviewViewModels;

    public interface IReviewService
    {
        public Task AddReview(ReviewInputModel model);

        public ReviewListViewModel GetReviews(string propertyId);
    }
}

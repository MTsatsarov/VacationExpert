namespace VacationExpert.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Common;
    using VacationExpert.Data.Models;
    using VacationExpert.Services.Data.ReviewServices;
    using VacationExpert.Services.Models;

    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewController(IReviewService reviewService, UserManager<ApplicationUser> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("api/review/addReview")]
        [IgnoreAntiforgeryToken]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> AddReview([FromBody] ReviewInputModel model)
        {
            model.UserId = this.userManager.GetUserId(this.User);
            await this.reviewService.AddReview(model);
            return this.Ok();
        }
    }
}

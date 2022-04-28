namespace VacationExpert.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Common;
    using VacationExpert.Services.Data.VoteService;
    using VacationExpert.Services.Models;

    [Authorize]
    [Route("api/[controller]")]
    public class VoteController : BaseController
    {
        private readonly IVoteService voteService;

        public VoteController(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        [HttpPost]

        [Route("Vote")]
        [IgnoreAntiforgeryToken]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> Vote([FromBody] VoteInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.UserId = userId;

            try
            {
                await this.voteService.Vote(model);
            }
            catch (System.Exception e)
            {
                return this.BadRequest(e.Message);
            }

            return this.Ok(model);
        }
    }
}

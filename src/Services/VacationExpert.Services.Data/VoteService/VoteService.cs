using System;
using System.Linq;
using System.Threading.Tasks;
using VacationExpert.Data;
using VacationExpert.Data.Models;
using VacationExpert.Services.Models;

namespace VacationExpert.Services.Data.VoteService
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext db;

        public VoteService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Vote(VoteInputModel model)
        {
            var review = this.db.Reviews.FirstOrDefault(x => x.Id == model.ReviewId);

            if (review.UserId == model.UserId)
            {
                throw new InvalidOperationException("You cannot vote for your own review");
            }

            if (review.Votes.Any(x => x.UserId == model.UserId))
            {
                var vote = review.Votes.FirstOrDefault(x => x.UserId == model.UserId);
                vote.Like = model.Like;
                this.db.Update(vote);
            }
            else
            {
                var entry = await this.db.AddAsync(new Vote { Like = model.Like, UserId = model.UserId, ReviewId = model.ReviewId });
                review.Votes.Add(entry.Entity);
                this.db.Reviews.Update(review);
            }

            await this.db.SaveChangesAsync();
        }
    }
}

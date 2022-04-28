namespace VacationExpert.Services.Data.VoteService
{
    using System.Threading.Tasks;
    using VacationExpert.Services.Models;

    public interface IVoteService
    {
        public Task Vote(VoteInputModel model);
    }
}

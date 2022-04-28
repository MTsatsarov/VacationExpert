namespace VacationExpert.Services.Models
{
    public class VoteInputModel
    {
        public string ReviewId { get; set; }

        public bool Like { get; set; }

        public string UserId { get; set; }
    }
}

namespace VacationExpert.Services.Models
{
    public class ReviewInputModel
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public string PropertyId { get; set; }

        public int Rating { get; set; }
    }
}

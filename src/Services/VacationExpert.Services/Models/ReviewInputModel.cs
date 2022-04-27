namespace VacationExpert.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ReviewInputModel
    {
        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        [Required]
        public string PropertyId { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }
    }
}

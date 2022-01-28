namespace VacationExpert.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BedInputModel
    {
        [Required]
        public string Type { get; set; }

        public int Count { get; set; }
    }
}

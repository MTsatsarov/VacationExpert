namespace VacationExpert.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ContactInputModel
    {
        [Required]
        [Display(Name = "Name of contact")]
        public string ContactName { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [Phone]
        [Display(Name = "Additional phone")]
        public string AdditionalPhone { get; set; }
    }
}

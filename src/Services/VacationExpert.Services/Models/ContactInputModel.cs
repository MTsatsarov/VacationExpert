namespace VacationExpert.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ContactInputModel
    {
        [Required]
        public string Name { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [Phone]
        public string AdditionalPhone { get; set; }
    }
}

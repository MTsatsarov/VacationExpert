namespace VacationExpert.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ServicesInputModel
    {
        [Required]
        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace VacationExpert.Services.Models
{
    public class ServicesInputModel
    {
        [Required]
        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}
namespace VacationExpert.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FacilityInputModel
    {
        public FacilityInputModel()
        {
            this.Services = new HashSet<string>();
        }

        [Required]
        public string Parking { get; set; }

        [Required]
        public string Breakfast { get; set; }

        [Required]
        public string Language { get; set; }

        public ICollection<string> Services { get; set; }
    }
}

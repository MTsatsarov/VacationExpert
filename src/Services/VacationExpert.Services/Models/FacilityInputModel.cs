namespace VacationExpert.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FacilityInputModel
    {
        public FacilityInputModel()
        {
            this.Services = new List<ServicesInputModel>();
        }

        [Required]
        public string Parking { get; set; }

        [Required]
        public string Breakfast { get; set; }

        [Required]
        public string Language { get; set; }

        public List<ServicesInputModel> Services { get; set; }
    }
}

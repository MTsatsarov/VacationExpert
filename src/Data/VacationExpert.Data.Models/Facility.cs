namespace VacationExpert.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VacationExpert.Data.Common.Models;
    using VacationExpert.Data.Models.Enums;

    public class Facility : BaseDeletableModel<int>
    {
        public Facility()
        {
            this.Languages = new HashSet<Language>();
            this.Services = new HashSet<Service>();
        }

        public Parking Parking { get; set; }

        public Breakfast Breakfast { get; set; }

        public ICollection<Language> Languages { get; set; }

        public ICollection<Service> Services { get; set; }

        [Required]
        public string PropertyId { get; set; }

        public Property Property { get; set; }
    }
}

namespace VacationExpert.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
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

        public virtual ICollection<Language> Languages { get; set; }

        public virtual ICollection<Service> Services { get; set; }

  
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }
    }
}

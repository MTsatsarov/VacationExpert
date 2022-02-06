namespace VacationExpert.Data.Models
{
    using System.Collections.Generic;

    using VacationExpert.Data.Common.Models;

    public class Service : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}

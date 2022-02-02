using System.Collections.Generic;
using VacationExpert.Data.Common.Models;

namespace VacationExpert.Data.Models
{
    public class Language:BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
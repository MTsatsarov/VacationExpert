using System.Collections.Generic;
using VacationExpert.Data.Common.Models;

namespace VacationExpert.Data.Models
{
    public class Service:BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
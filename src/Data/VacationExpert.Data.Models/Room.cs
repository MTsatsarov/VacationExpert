namespace VacationExpert.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VacationExpert.Data.Common.Models;
    using VacationExpert.Data.Models.Enums;

    public class Room : BaseDeletableModel<int>
    {
        public Room()
        {
            this.Beds = new HashSet<Bed>();
        }

        public string Type { get; set; }

        public SmokingPolicy SmookingPolicy { get; set; }

        [Range(1, 1000)]
        public int RoomCount { get; set; }

        [Range(1, 24)]
        public int TotalGuestsCount { get; set; }

        public int? Size { get; set; }

        public virtual ICollection<Bed> Beds { get; set; }

        [Required]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }
    }
}

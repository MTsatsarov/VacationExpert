namespace VacationExpert.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VacationExpert.Data.Common.Models;

    public class Property : BaseDeletableModel<string>
    {
        public Property()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Rooms = new HashSet<Room>();
            this.Photos = new HashSet<Photo>();
        }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public ICollection<Photo> Photos { get; set; }

        [Required]
        public string ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }
    }
}

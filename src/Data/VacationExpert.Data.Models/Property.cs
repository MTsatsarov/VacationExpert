namespace VacationExpert.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VacationExpert.Data.Common.Models;

    public class Property : BaseDeletableModel<string>
    {
        public Property()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Rooms = new HashSet<Room>();
            this.Photos = new HashSet<Photo>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        [Required]
        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        [ForeignKey("Facility")]
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}

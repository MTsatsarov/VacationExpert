namespace VacationExpert.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class PropertyUpdateModel
    {
        public PropertyUpdateModel()
        {
            this.Rooms = new List<RoomInputModel>();
        }

        public string Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public ContactInputModel Contact { get; set; }

        public string UserId { get; set; }

        public AddressInputModel Address { get; set; }

        public List<RoomInputModel> Rooms { get; set; }

        public FacilityInputModel Facilities { get; set; }
        public IFormCollection Images { get; set; }
    }
}

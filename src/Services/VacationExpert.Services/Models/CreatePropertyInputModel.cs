namespace VacationExpert.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using VacationExpert.Data.Models.Enums;

    public class CreatePropertyInputModel
    {
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

        public IFormFileCollection Images { get; set; }
    }
}

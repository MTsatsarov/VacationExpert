namespace VacationExpert.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreatePropertyInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public ContactInputModel Contact { get; set; }

        public AddressInputModel Address { get; set; }

        public List<RoomInputModel> Rooms { get; set; }

        public FacilityInputModel Facilities { get; set; }

        public ICollection<IFormFile> Images { get; set; }
    }
}

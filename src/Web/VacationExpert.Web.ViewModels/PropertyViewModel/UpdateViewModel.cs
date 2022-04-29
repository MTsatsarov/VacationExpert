namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VacationExpert.Services.Models;

    public class UpdateViewModel
    {
        public UpdateViewModel()
        {
            this.Rooms = new List<RoomInputModel>();
            this.Images = new List<string>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public ContactInputModel Contact { get; set; }

        public string UserId { get; set; }

        public AddressInputModel Address { get; set; }

        public List<RoomInputModel> Rooms { get; set; }

        public FacilityInputModel Facilities { get; set; }
        public List<string> Images { get; set; }
    }
}

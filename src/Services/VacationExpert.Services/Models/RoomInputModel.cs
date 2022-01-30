namespace VacationExpert.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RoomInputModel
    {
        [Required]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Smoking Policy")]
        public string SmokingPolicy { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name ="Room Count")]
        public int RoomCount { get; set; }

        public List<BedInputModel> Beds { get; set; }

        [Display(Name = "Total Guests")]
        public int GuestsCount { get; set; }

        [Display(Name = "Room Size")]
        public int? RoomSize { get; set; }
    }
}

namespace VacationExpert.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RoomInputModel
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string SmokingPolicy { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Count { get; set; }

        public ICollection<BedInputModel> Beds { get; set; }

        public int GuestsCount { get; set; }

        public int? RoomSize { get; set; }
    }
}

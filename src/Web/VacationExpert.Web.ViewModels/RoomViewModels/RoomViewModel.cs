namespace VacationExpert.Web.ViewModels.RoomViewModels
{
    using System.Collections.Generic;

    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Web.ViewModels.BedModels;

    public class RoomViewModel
    {
        public RoomViewModel()
        {
            this.Beds = new HashSet<BedViewModel>();
        }

        public string Type { get; set; }

        public SmokingPolicy SmokingPolicy { get; set; }

        public int RoomCount { get; set; }

        public int? Size { get; set; }

        public int GuestCount { get; set; }

        public ICollection<BedViewModel> Beds { get; set; }
    }
}

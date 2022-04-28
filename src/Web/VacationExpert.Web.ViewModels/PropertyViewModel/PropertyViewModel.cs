namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    using System.Collections.Generic;
    using VacationExpert.Web.ViewModels.ReviewViewModels;
    using VacationExpert.Web.ViewModels.RoomViewModels;

    public class PropertyViewModel
    {
        public PropertyViewModel()
        {
            this.ReviewCollection = new ReviewListViewModel();
            this.Rooms = new HashSet<RoomViewModel>();
            this.Services = new List<string>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Rating { get; set; }
        public string Owner { get; set; }

        public PropertyAddressViewModel Address { get; set; }

        public string Description { get; set; }

        public ICollection<RoomViewModel> Rooms { get; set; }

        public ICollection<string> Images { get; set; }

        public List<string> Services { get; set; }

        public ReviewListViewModel ReviewCollection { get; set; }
    }
}

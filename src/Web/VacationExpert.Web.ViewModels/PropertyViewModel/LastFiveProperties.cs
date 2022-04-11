using System.Collections.Generic;

namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    public class LastFiveProperties
    {

        public string Id { get; set; }

        public string CityName { get; set; }

        public string Name { get; set; }

        public ICollection<string> Images { get; set; }
    }
}

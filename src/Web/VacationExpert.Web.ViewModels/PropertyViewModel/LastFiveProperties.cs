namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    using System.Collections.Generic;

    public class LastFiveProperties
    {

        public string Id { get; set; }

        public string CityName { get; set; }

        public string Name { get; set; }

        public ICollection<string> Images { get; set; }
    }
}

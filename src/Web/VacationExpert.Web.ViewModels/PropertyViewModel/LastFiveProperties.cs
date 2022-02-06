namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    using VacationExpert.Data.Models;
    using VacationExpert.Services.Mapping;

    public class LastFiveProperties : IMapFrom<Property>
    {
        public string CityName { get; set; }

        public int PropertiesCount { get; set; }
    }
}

using VacationExpert.Data.Models;
using VacationExpert.Services.Mapping;

namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    public class LastFiveProperties: IMapFrom<Property>
    {
        public string CityName { get; set; }

        public int PropertiesCount { get; set; }
    }
}

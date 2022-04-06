namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    using VacationExpert.Data.Models;
    using VacationExpert.Services.Mapping;

    public class LastFiveProperties
    {

        public string Id { get; set; }

        public string CityName { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }
    }
}

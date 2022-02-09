namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    using System.Collections.Generic;

    public class PropertyViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public PropertyAddressViewModel Address { get; set; }

        public string Description { get; set; }

        public ICollection<string> Images { get; set; }

        public ICollection<string> Facilities { get; set; }
    }
}

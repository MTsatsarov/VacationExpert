namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    using System.Collections.Generic;

    public class DefaultPropertyCreateViewModel
    {
        public DefaultPropertyCreateViewModel()
        {
            this.Services = new List<string>();
        }

        public ICollection<string> Services { get; set; }
    }
}

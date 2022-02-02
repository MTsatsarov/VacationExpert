namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    using System;
    using System.Collections.Generic;

    using VacationExpert.Data.Models.Enums;

    public class DefaultPropertyCreateViewModel
    {
        public DefaultPropertyCreateViewModel()
        {
            this.Services = new List<string>();
        }

        public ICollection<string> Services { get; set; }
    }
}

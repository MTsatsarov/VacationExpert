using System;
using System.Collections.Generic;

using VacationExpert.Data.Models.Enums;

namespace VacationExpert.Web.ViewModels.PropertyViewModel
{

    public class DefaultPropertyCreateViewModel
    {
        public DefaultPropertyCreateViewModel()
        {
            this.Services = new List<string>();
        }

        public ICollection<string> Services { get; set; }
    }
}

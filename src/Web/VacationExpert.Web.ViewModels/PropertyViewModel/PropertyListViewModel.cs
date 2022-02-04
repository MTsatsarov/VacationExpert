using System;
using System.Collections.Generic;
using System.Text;

namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    public class PropertyListViewModel
    {
        public IEnumerable<PropertyInListViewModel> Properties { get; set; }
    }
}

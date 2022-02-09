namespace VacationExpert.Services.Data.PropertyServices
{
    using System.Collections.Generic;

    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public interface IPropertyService
    {
        public IEnumerable<T> GetLastFIve<T>();

        public PropertyViewModel GetProperty(string id);
    }
}

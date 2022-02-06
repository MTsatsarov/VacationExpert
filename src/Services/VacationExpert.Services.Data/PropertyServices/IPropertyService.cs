namespace VacationExpert.Services.Data.PropertyServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public interface IPropertyService
    {
        public Task Create(CreatePropertyInputModel model);

        public IEnumerable<T> GetLastFIve<T>();

        public PropertyViewModel GetProperty(string id);
    }
}

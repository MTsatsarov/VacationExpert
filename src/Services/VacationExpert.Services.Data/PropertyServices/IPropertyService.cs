namespace VacationExpert.Services.Data.PropertyServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public interface IPropertyService
    {
        public PropertyListViewModel GetLastFIve();

        public PropertyViewModel GetProperty(string id);

        public CreatePropertyInputModel GetUpdateModel(string id);

        public PropertyListViewModel GetByUser(string userId, int page);

        public Task Delete(string userId, string propertyId);

        public Task Update(CreatePropertyInputModel model);
    }
}

﻿namespace VacationExpert.Services.Data.PropertyServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public interface IPropertyService
    {
        public IEnumerable<T> GetLastFIve<T>();

        public PropertyViewModel GetProperty(string id);

        public PropertyListViewModel GetByUser(string userId,int page);
        public Task Delete(string userId, string propertyId);

    }
}

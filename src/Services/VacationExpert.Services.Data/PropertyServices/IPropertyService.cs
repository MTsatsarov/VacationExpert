namespace VacationExpert.Services.Data.PropertyServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VacationExpert.Services.Models;

    public interface IPropertyService
    {
        public Task Create(CreatePropertyInputModel model);

        public IEnumerable<T> GetLastFIve<T>();
    }
}
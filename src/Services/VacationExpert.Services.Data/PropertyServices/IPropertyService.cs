namespace VacationExpert.Services.Data.PropertyServices
{
    using System.Threading.Tasks;

    using VacationExpert.Services.Models;

    public interface IPropertyService
    {
        public Task Create(CreatePropertyInputModel model);
    }
}

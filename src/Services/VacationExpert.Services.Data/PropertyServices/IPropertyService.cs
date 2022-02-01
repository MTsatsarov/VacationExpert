using VacationExpert.Services.Models;

namespace VacationExpert.Services.Data.PropertyServices
{
    public interface IPropertyService
    {
        public void Create(CreatePropertyInputModel model);
    }

}


namespace VacationExpert.Services.Data.PropertyServices
{
    using System.Threading.Tasks;

    using VacationExpert.Data.Models;
    using VacationExpert.Services.Models;

    public interface IPropertyStoreService
    {
        public Task Create(CreatePropertyInputModel model);

        public void AddAddress(CreatePropertyInputModel model, Property inputModel);

        public void AddContact(CreatePropertyInputModel model, Property inputModel);

        public void AddFacilities(CreatePropertyInputModel model, Property inputModel);

        public void AddRooms(CreatePropertyInputModel model, Property inputModel);
    }
}

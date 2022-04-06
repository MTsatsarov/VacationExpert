namespace VacationExpert.Services.Data.PropertyService.Services
{
    using System.Collections.Generic;

    public interface IPropertyServices
    {
      public ICollection<string> GetServices();
    }
}

namespace VacationExpert.Services.Data.PropertyService.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using VacationExpert.Data;

    public class PropertyServices : IPropertyServices
    {
        private readonly ApplicationDbContext db;

        public PropertyServices(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<string> GetServices()
        {
            return this.db.Services.Select(x => x.Name).ToList();
        }
    }
}

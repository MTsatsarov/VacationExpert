namespace VacationExpert.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VacationExpert.Data.Models;

    public class ServiceSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var services = new List<string>() { "Free Wifi", "Non-smoking rooms", "Restaurant", "Airport shuttle", "Room service", "Family rooms", "Bar", "Spa", "24-hour front desk", "Hot tub/Jacuzzi", "Sauna", "Air Conditioning", "Fitness Center", "Water Park", "Garden", "Swimming Pool", "Terrace" };

            foreach (var service in services)
            {
                if (dbContext.Services.Any(x => x.Name == service))
                {
                    continue;
                }
                await SeedServiceAsync(dbContext, service);
            }
        }

        private static async Task SeedServiceAsync(ApplicationDbContext db, string service)
        {
            await db.AddAsync(new Service() { Name = service });
            await db.SaveChangesAsync();
        }
    }
}

namespace VacationExpert.Services.Data.PropertyServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using VacationExpert.Data;
    using VacationExpert.Data.Models;
    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Services.Data.ImageService;
    using VacationExpert.Services.Models;

    public class PropertyService : IPropertyService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IImageService imageService;

        public PropertyService(ApplicationDbContext dbContext, IImageService imageService)
        {
            this.dbContext = dbContext;
            this.imageService = imageService;
        }

        public async Task Create(CreatePropertyInputModel model)
        {
            var inputModel = new Property();
            inputModel.Name = model.Name;
            var address = new Address()
            {
                City = (City)Enum.Parse(typeof(City), model.Address.City),
                Country = (Country)Enum.Parse(typeof(Country), model.Address.Country),
                StreetAddress = model.Address.StreetAddress,
                ZipCode = model.Address.ZipCode,
            };
            inputModel.Address = address;

            var contact = new Contact()
            {
                Name = model.Contact.ContactName,
                Phone = model.Contact.Phone,
                AdditionalPhone = model.Contact.AdditionalPhone,
            };
            inputModel.Contact = contact;

            var facility = new Facility()
            {
                Breakfast = (Breakfast)Enum.Parse(typeof(Breakfast), model.Facilities.Breakfast),
                Parking = (Parking)Enum.Parse(typeof(Parking), model.Facilities.Parking),
            };

            facility.Languages.Add(new VacationExpert.Data.Models.Language { Name = model.Facilities.Language });

            var selectedServices = model.Facilities.Services.Where(x => x.Selected == true);

            foreach (var service in selectedServices)
            {
                facility.Services.Add(new VacationExpert.Data.Models.Service { Name = service.Name });
            }

            inputModel.Facility = facility;

            foreach (var room in model.Rooms)
            {
                var currentRoom = new Room()
                {
                    Type = (RoomType)Enum.Parse(typeof(RoomType), room.Type),
                    RoomCount = room.RoomCount,
                    Size = room.RoomSize,
                    TotalGuestsCount = room.GuestsCount,
                    SmookingPolicy = (SmokingPolicy)Enum.Parse(typeof(SmokingPolicy), room.SmokingPolicy),
                };
                foreach (var bed in room.Beds)
                {
                    var currentBed = new Bed()
                    {
                        Count = bed.Count,
                        Type = (BedType)Enum.Parse(typeof(BedType), bed.Type),
                    };
                    currentRoom.Beds.Add(currentBed);
                }

                inputModel.Rooms.Add(currentRoom);
            }

            var images = model.Images
                .Select(x => new ImageInputModel
                {
                    Name = x.Name,
                    Type = x.ContentType,
                    Content = x.OpenReadStream(),
                }).ToList();

            var resultedImages = await this.imageService.ImageProcess(images);
            inputModel.Images = resultedImages.ToList();

            await this.dbContext.Properties.AddAsync(inputModel);
            await this.dbContext.SaveChangesAsync();
        }
    }
}

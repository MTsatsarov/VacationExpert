namespace VacationExpert.Services.Data.PropertyServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VacationExpert.Data;
    using VacationExpert.Data.Models;
    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Services.Data.ImageService;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public class PropertyService : IPropertyService, IPropertyStoreService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IImageService imageService;

        public PropertyService(ApplicationDbContext dbContext, IImageService imageService)
        {
            this.dbContext = dbContext;
            this.imageService = imageService;
        }

        public void AddAddress(CreatePropertyInputModel model, Property inputModel)
        {
            inputModel.Address = new Address()
            {
                City = (City)Enum.Parse(typeof(City), model.Address.City),
                Country = (Country)Enum.Parse(typeof(Country), model.Address.Country),
                StreetAddress = model.Address.StreetAddress,
                ZipCode = model.Address.ZipCode,
            };
        }

        public void AddContact(CreatePropertyInputModel model, Property inputModel)
        {
            var contact = new Contact()
            {
                Name = model.Contact.ContactName,
                Phone = model.Contact.Phone,
                AdditionalPhone = model.Contact.AdditionalPhone,
                PropertyId = inputModel.Id,
            };
            inputModel.Contact = contact;
        }

        public void AddFacilities(CreatePropertyInputModel model, Property inputModel)
        {
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
        }

        public void AddRooms(CreatePropertyInputModel model, Property inputModel)
        {
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
        }

        public async Task Create(CreatePropertyInputModel model)
        {
            var inputModel = new Property();
            inputModel.UserId = model.UserId;
            inputModel.Name = model.Name;
            inputModel.Rating = (int)model.Rating;
            this.AddAddress(model, inputModel);
            this.AddContact(model, inputModel);
            this.AddFacilities(model, inputModel);
            this.AddRooms(model, inputModel);
            await this.AddImages(model, inputModel);

            await this.dbContext.Properties.AddAsync(inputModel);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetLastFIve<T>()
        {
            throw new NotImplementedException();
        }

        public PropertyViewModel GetProperty(string id)
        {
            var result = this.dbContext.Properties.Where(x => x.Id == id).FirstOrDefault();
            var model = new PropertyViewModel();
            model.Id = result.Id;
            model.Name = result.Name;
            model.Address = new PropertyAddressViewModel
            {
                Street = result.Address.StreetAddress,
                City = result.Address.City.ToString(),
                Country = result.Address.Country.ToString(),
                Grade = result.Reviews.Count > 0 ? ((double)Math.Round(result.Reviews.Average(x => x.Rating), 2)) : (double)(0.0),
            };
            model.Facilities = result.Facility.Services.Select(x => x.Name).ToList();
            model.Images = this.imageService.GetAllImages(result.Id).ToList();

            return model;
        }

        private async Task AddImages(CreatePropertyInputModel model, Property inputModel)
        {
            var images = model.Images
                .Select(x => new ImageInputModel
                {
                    Name = x.Name,
                    Type = x.ContentType,
                    Content = x.OpenReadStream(),
                }).ToList();

            var resultedImages = await this.imageService.ImageProcess(images);
            foreach (var image in resultedImages)
            {
                this.dbContext.Images.Add(image);
            }

            inputModel.Images = resultedImages.ToList();
        }
    }
}

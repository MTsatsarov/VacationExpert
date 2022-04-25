namespace VacationExpert.Services.Data.PropertyServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VacationExpert.Common;
    using VacationExpert.Data;
    using VacationExpert.Data.Models;
    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Services.Data.ImageService;
    using VacationExpert.Services.Data.ReviewServices;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.BedModels;
    using VacationExpert.Web.ViewModels.PropertyViewModel;
    using VacationExpert.Web.ViewModels.RoomViewModels;

    public class PropertyService : IPropertyService, IPropertyStoreService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IImageService imageService;
        private readonly IReviewService reviewService;

        public PropertyService(ApplicationDbContext dbContext, IImageService imageService, IReviewService reviewService)
        {
            this.dbContext = dbContext;
            this.imageService = imageService;
            this.reviewService = reviewService;
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
            inputModel.Description = model.Description;
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

        public async Task Delete(string userId, string propertyId)
        {
            if (userId == null || !this.dbContext.Properties.Any(x => x.UserId == userId))
            {
                throw new InvalidOperationException("User not found");
            }
            if (propertyId == null || !this.dbContext.Properties.Any(x => x.Id == propertyId))
            {
                throw new InvalidOperationException("Property not found");
            }
            var property = this.dbContext.Properties.FirstOrDefault(x => x.Id == propertyId);
            property.IsDeleted = true;
            property.DeletedOn = DateTime.UtcNow;
            this.dbContext.Properties.Update(property);
            await this.dbContext.SaveChangesAsync();
        }

        public PropertyListViewModel GetByUser(string userId, int page = 1)
        {
            var model = new PropertyListViewModel();

            var properties = this.dbContext.Properties.Where(x => x.UserId == userId).Select(x => new PropertyInListViewModel()
            {
                City = x.Address.City.ToString(),
                ImageId = x.Images.Select(x => x.Id).First().ToString(),
                Id = x.Id,
                Name = x.Name,
                Rating = x.Rating,
                Grade = x.Reviews.Average(x => x.Rating).ToString("F2"),
                ReviewsCount = x.Reviews.Count,
                UserId = userId,
            }).ToList();

            if (properties.Count == 0)
            {
                model.Properties = new List<PropertyInListViewModel>();
                return model;
            }

            model.TotalPages = (int)Math.Ceiling((double)properties.Count() / (double)GlobalConstants.PropertiesPerPage);
            if (page < 1 || page > model.TotalPages)
            {
                throw new InvalidOperationException("Invalid page");
            }

            properties = properties.Skip((page - 1) * GlobalConstants.PropertiesPerPage).Take(GlobalConstants.PropertiesPerPage).ToList();
            model.CurrentPage = page;

            model.Properties = properties.ToList();

            return model;
        }

        public PropertyListViewModel GetLastFIve()
        {
            var properties = this.dbContext.Properties.OrderByDescending(x => x.CreatedOn).Select(x => new PropertyInListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                City = x.Address.City.ToString(),
                Rating = x.Rating,
                ImageId = x.Images.Select(x => x.Id).First().ToString(),
                ReviewsCount = x.Reviews.Count(),
                Grade = x.Reviews.Count > 0 ? x.Reviews.Average(x => x.Rating).ToString("F2") : "0",
            }).Take(5).ToList();
            var totalPages = (int)Math.Ceiling((double)properties.Count() / (double)GlobalConstants.PropertiesPerPage);
            var model = new PropertyListViewModel();
            model.Properties = new List<PropertyInListViewModel>(properties);
            return model;
        }

        public PropertyViewModel GetProperty(string id)
        {
            var property = this.dbContext.Properties.Where(x => x.Id == id).FirstOrDefault();
            var viewModel = new PropertyViewModel();
            viewModel.Id = property.Id;
            viewModel.Name = property.Name;
            viewModel.Address = new PropertyAddressViewModel
            {
                Street = property.Address.StreetAddress,
                City = property.Address.City.ToString(),
                Country = property.Address.Country.ToString(),
                Grade = property.Reviews.Count > 0 ? ((double)Math.Round(property.Reviews.Average(x => x.Rating), 2)) : (double)0.0,
            };

            foreach (var room in property.Rooms)
            {
                var currentRoom = new RoomViewModel()
                {
                    SmokingPolicy = room.SmookingPolicy,
                    Size = room.Size,
                    Type = room.Type,
                    RoomCount = room.RoomCount,
                    GuestCount = room.TotalGuestsCount,
                };

                foreach (var bed in room.Beds)
                {
                    var currentBed = new BedViewModel()
                    {
                        Type = bed.Type,
                        Count = bed.Count,
                    };
                    currentRoom.Beds.Add(currentBed);
                }

                viewModel.Rooms.Add(currentRoom);
            }

            var a = property.Reviews;
            viewModel.ReviewCollection = this.reviewService.GetReviews(id, "1");
            var services = property.Facility.Services.Select(x => x.Name).ToList();
            viewModel.Facilities = services.Select(x => Enum.Parse<VacationExpert.Data.Models.Enums.Service>(x)).ToList();
            viewModel.Images = this.imageService.GetAllImages(property.Id).ToList();

            return viewModel;
        }

        public CreatePropertyInputModel GetUpdateModel(string id)
        {
            var model = new CreatePropertyInputModel();
            var property = this.dbContext.Properties.Where(x => x.Id == id).FirstOrDefault();

            model.Id = property.Id;
            model.UserId = property.UserId;
            model.Name = property.Name;
            model.Rating = (Rating)Enum.Parse(typeof(Rating), property.Rating.ToString());
            model.Contact = new ContactInputModel()
            {
                ContactName = property.Contact.Name,
                Phone = property.Contact.Phone,
                AdditionalPhone = property.Contact.AdditionalPhone,
            };
            model.Address = new AddressInputModel()
            {
                City = property.Address.City.ToString(),
                Country = property.Address.Country.ToString(),
                StreetAddress = property.Address.StreetAddress.ToString(),
                ZipCode = property.Address.ZipCode.ToString(),
            };
            model.Rooms = property.Rooms.Select(x => new RoomInputModel
            {
                Type = x.Type.ToString(),
                SmokingPolicy = x.SmookingPolicy.ToString(),
                RoomSize = x.Size,
                RoomCount = x.RoomCount,
                GuestsCount = x.TotalGuestsCount,
                Beds = x.Beds.Select(x => new BedInputModel
                {
                    Count = x.Count,
                    Type = x.Type.ToString(),
                }).ToList(),
            }).ToList();
            model.Facilities = new FacilityInputModel()
            {
                Breakfast = property.Facility.Breakfast.ToString(),
                Language = property.Facility.Languages.ToString(),
                Parking = property.Facility.Parking.ToString(),
                Services = property.Facility.Services.Select(y => new ServicesInputModel()
                {
                    Name = y.Name,
                    Selected = true,
                }).ToList(),
            };
            return model;
        }

        public async Task Update(CreatePropertyInputModel model)
        {
            var property = this.dbContext.Properties.Where(x => x.Id == model.Id).FirstOrDefault();
            property.Name = model.Name;

            this.dbContext.Update(property);
            await this.dbContext.SaveChangesAsync();
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

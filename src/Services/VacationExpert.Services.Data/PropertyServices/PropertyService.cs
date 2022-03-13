﻿namespace VacationExpert.Services.Data.PropertyServices
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
    using VacationExpert.Web.ViewModels.ReviewViewModels;
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
                throw new InvalidOperationException("Sorry you haven't create any properties.");
            }

            model.TotalPages = (int)Math.Ceiling((double)properties.Count() / (double)GlobalConstants.PropertiesPerPage);
            if (page == 0 || page > model.TotalPages)
            {
                throw new InvalidOperationException("Invalid page");
            }

            properties = properties.Skip((page - 1) * GlobalConstants.PropertiesPerPage).Take(GlobalConstants.PropertiesPerPage).ToList();
            model.CurrentPage = page;

            model.Properties = properties.ToList();

            return model;
        }

        public IEnumerable<T> GetLastFIve<T>()
        {
            throw new NotImplementedException();
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
            viewModel.Facilities = property.Facility.Services.Select(x => x.Name).ToList();
            viewModel.Images = this.imageService.GetAllImages(property.Id).ToList();

            return viewModel;
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

namespace VacationExpert.Services.Data.SearchService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VacationExpert.Common;
    using VacationExpert.Data;
    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Services.Data.ImageService;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IImageService imageService;

        public SearchService(ApplicationDbContext dbContext, IImageService imageService)
        {
            this.dbContext = dbContext;
            this.imageService = imageService;
        }

        public async Task<PropertyListViewModel> GetResults(SearchInputModel model, int page)
        {

            var list = new PropertyListViewModel();
            var city = (City)Enum.Parse(typeof(City), model.City);
            var properties = this.dbContext.Properties.Where(x => x.Address.City == city).Skip((page - 1) * GlobalConstants.PropertiesPerPage).Take(GlobalConstants.PropertiesPerPage).ToList();
            var totalPages = (int)Math.Ceiling((double)properties.Count() / (double)GlobalConstants.PropertiesPerPage);

            if (properties.Count == 0)
            {
                throw new InvalidOperationException("Sorry no properties found for this city");
            }

            if (page == 0)
            {
                throw new InvalidOperationException("Invalid page");
            }

            var propertiesModel = new List<PropertyInListViewModel>();

            list.CurrentPage = page;
            list.TotalPages = properties.Count();

            foreach (var property in properties)
            {
                var currentModel = new PropertyInListViewModel();
                currentModel.Id = property.Id;
                currentModel.Name = property.Name;
                currentModel.City = property.Address.City.ToString();
                currentModel.Rating = property.Rating;
                currentModel.ImageId = property.Images.Select(x => x.Id).First().ToString();
                if (property.Reviews.Count > 0)
                {
                    currentModel.Grade = property.Reviews.Average(x => x.Rating).ToString("F2") ?? "0";
                }
                else
                {
                    currentModel.Grade = "0";
                }

                currentModel.ReviewsCount = property.Reviews.Count();
                propertiesModel.Add(currentModel);
            }

            list.Properties = propertiesModel.ToList();
            return list;
        }

        public ICollection<string> GetSuggestions(string terms)
        {
            var cities = Enum.GetNames(typeof(City)).Where(x => x.ToLower().Contains(terms.ToLower())).ToList();
            return cities;
        }
    }
}

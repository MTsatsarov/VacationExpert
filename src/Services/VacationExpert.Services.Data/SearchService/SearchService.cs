namespace VacationExpert.Services.Data.SearchService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using VacationExpert.Data;
    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Services.Data.ImageService;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IImageService imageService;
        private const int PropertiesPerPage = 10;

        public SearchService(ApplicationDbContext dbContext, IImageService imageService)
        {
            this.dbContext = dbContext;
            this.imageService = imageService;
        }

        public async Task<PropertyListViewModel> GetResults(SearchInputModel model, int page)
        {

            var list = new PropertyListViewModel();
            var city = (City)Enum.Parse(typeof(City), model.City);
            var properties = this.dbContext.Properties.Where(x => x.Address.City == city).ToList();
            var totalPages = (int)Math.Ceiling((double)properties.Count() / (double)PropertiesPerPage);

            if (page > totalPages || page == 0)
            {
                throw new InvalidOperationException();
            }

            var propertiesModel = new List<PropertyInListViewModel>();

            list.CurrentPage = page;
            list.TotalPages = properties.Count();

            foreach (var property in properties.Skip((page - 1) * PropertiesPerPage).Take(PropertiesPerPage))
            {
                var currentModel = new PropertyInListViewModel
                {
                    Id = property.Id,
                    Name = property.Name,
                    City = property.Address.City.ToString(),
                    Rating = property.Rating,
                    ImageId = property.Images.Select(x => x.Id).First().ToString(),
                    Grade = property.Reviews.Average(x => x.Rating).ToString("F2"),
                    Reviews = property.Reviews.Count(),
                };
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

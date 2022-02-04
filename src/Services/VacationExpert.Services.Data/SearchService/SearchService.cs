namespace VacationExpert.Services.Data.SearchService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using VacationExpert.Data;
    using VacationExpert.Data.Models.Enums;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext dbContext;

        public SearchService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public PropertyListViewModel GetResults(SearchInputModel model)
        {
            var list = new PropertyListViewModel();
            var city = (City)Enum.Parse(typeof(City), model.City);
            var properties = this.dbContext.Properties.Where(x => x.Address.City == city).ToList();
            var propertiesModel = new List<PropertyInListViewModel>();

            foreach (var property in properties)
            {
                var currentModel = new PropertyInListViewModel()
                {
                };
                currentModel.Name = property.Name;
                currentModel.City = property.Address.City.ToString();
                currentModel.Image = property.Images.Select(x => x.ThumbnailContent).FirstOrDefault();
                currentModel.Rating = property.Rating.ToString();

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

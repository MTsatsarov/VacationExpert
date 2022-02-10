namespace VacationExpert.Services.Data.SearchService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public interface ISearchService
    {
        public ICollection<string> GetSuggestions(string terms);

        public Task<PropertyListViewModel> GetResults(SearchInputModel model, int page);
    }
}

namespace VacationExpert.Services.Data.SearchService
{
    using System.Collections.Generic;

    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public interface ISearchService
    {
        public ICollection<string> GetSuggestions(string terms);
        public PropertyListViewModel GetResults(SearchInputModel model);
    }
}

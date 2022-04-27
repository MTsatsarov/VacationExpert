namespace VacationExpert.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VacationExpert.Services.Data.ImageService;
    using VacationExpert.Services.Data.SearchService;
    using VacationExpert.Services.Models;
    using VacationExpert.Web.ViewModels;
    using VacationExpert.Web.ViewModels.PropertyViewModel;

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;
        private readonly IImageService service;

        public SearchController(ISearchService searchService, IImageService service)
        {
            this.searchService = searchService;
            this.service = service;
        }

        [HttpGet]
        [Route("/search/Results/page")]
        public async Task<IActionResult> Results(SearchInputModel model, int id = 1)
        {
            var result = new PropertyListViewModel();
            try
            {
                result = await this.searchService.GetResults(model, id);
            }
            catch (Exception)
            {
                return this.View(new PropertyListViewModel()
                {
                    Properties = new List<PropertyInListViewModel>(),
                });
            }

            return this.View(result);
        }

        [HttpPost]
        [Route("api/search/suggestions")]
        [IgnoreAntiforgeryToken]
        public IActionResult GetSuggestions([FromBody] SuggestionInputModel model)
        {
            var result = this.searchService.GetSuggestions(model.Name);
            return this.Json(result);
        }
    }
}

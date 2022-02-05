using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VacationExpert.Services.Data.ImageService;
using VacationExpert.Services.Data.SearchService;
using VacationExpert.Services.Models;

namespace VacationExpert.Web.Controllers
{
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
        public async Task<IActionResult> Results(SearchInputModel model)
        {
            var result = await this.searchService.GetResults(model);
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

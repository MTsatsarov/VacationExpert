namespace VacationExpert.Web.ViewModels.PropertyViewModel
{
    using System.Collections.Generic;

    public class PropertyListViewModel
    {
        public IEnumerable<PropertyInListViewModel> Properties { get; set; }

        public int CurrentPage { get; set; }

        public bool HasPreviousPage => this.CurrentPage > 1;

        public bool HasNextPage => this.CurrentPage < this.TotalPages;

        public int PreviousPageNumber => this.CurrentPage - 1;

        public int NextPageNumber => this.CurrentPage + 1;

        public int TotalPages { get; set; }
    }
}

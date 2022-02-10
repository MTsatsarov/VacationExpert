namespace VacationExpert.Web.ViewModels.ReviewViewModels
{
    using System.Collections.Generic;

    public class ReviewListViewModel
    {
        public ReviewListViewModel()
        {
            this.Reviews = new List<ReviewInListViewModel>();
        }

        public List<ReviewInListViewModel> Reviews { get; set; }

        public int TotalPages { get; set; }

        public bool HasNextPage => this.CurrentPage < this.TotalPages;

        public bool HasPreviousPage => this.CurrentPage > 1;

        public int CurrentPage { get; set; }

    }
}

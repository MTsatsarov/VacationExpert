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
    }
}

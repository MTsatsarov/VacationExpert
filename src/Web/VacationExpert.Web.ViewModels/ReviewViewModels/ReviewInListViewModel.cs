namespace VacationExpert.Web.ViewModels.ReviewViewModels
{
    public class ReviewInListViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public double Rating { get; set; }

        public string Username { get; set; }

        public string DateTime { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }

    }
}

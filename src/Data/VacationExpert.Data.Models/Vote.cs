namespace VacationExpert.Data.Models
{
    using System;

    using VacationExpert.Data.Common.Models;

    public class Vote : BaseDeletableModel<string>
    {
        public Vote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public bool Like { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ReviewId { get; set; }

        public virtual Review Review { get; set; }
    }
}

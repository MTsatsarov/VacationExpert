namespace VacationExpert.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VacationExpert.Data.Common.Models;

    public class Review : BaseDeletableModel<string>
    {
        public Review()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Votes = new List<Vote>();
        }

        [Required]
        public string Content { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}

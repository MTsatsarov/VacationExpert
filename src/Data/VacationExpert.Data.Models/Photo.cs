namespace VacationExpert.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VacationExpert.Data.Common.Models;

    public class Photo : BaseDeletableModel<string>
    {
        public Photo() => this.Id = Guid.NewGuid().ToString();

        public string Extension { get; set; }

        [Required]
        public string PropertyId { get; set; }


        public Property Property { get; set; }
    }
}

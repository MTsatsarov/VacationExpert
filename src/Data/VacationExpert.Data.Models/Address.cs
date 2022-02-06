namespace VacationExpert.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using VacationExpert.Data.Common.Models;
    using VacationExpert.Data.Models.Enums;

    public class Address : BaseDeletableModel<int>
    {
        [Required]
        public string StreetAddress { get; set; }

        public Country Country { get; set; }

        public City City { get; set; }

        public string ZipCode { get; set; }

        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }
    }
}

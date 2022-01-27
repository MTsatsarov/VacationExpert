namespace VacationExpert.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using VacationExpert.Data.Common.Models;

    public class Contact : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [Phone]
        public string AdditionalPhone { get; set; }

        [Required]
        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }
    }
}

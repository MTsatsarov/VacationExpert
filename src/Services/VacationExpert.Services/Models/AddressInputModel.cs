namespace VacationExpert.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AddressInputModel
    {
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }
}

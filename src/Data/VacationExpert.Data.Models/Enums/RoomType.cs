namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum RoomType
    {
        Single = 1,
        Double = 2,
        Twin = 3,
        [Display(Name = "Twin/Double")]
        TwinDouble = 4,
        Triple = 5,
        Quad = 6,
        Family = 7,
        Suit = 8,
        Studio = 9,
        Apartment = 10,
    }
}

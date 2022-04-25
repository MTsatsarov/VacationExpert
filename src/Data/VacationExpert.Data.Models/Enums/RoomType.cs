namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum RoomType
    {
        [Description("Single")]
        Single = 1,
        [Description("Double")]
        Double = 2,
        [Description("Twin")]
        Twin = 3,
        [Description("Twin/Double")]
        TwinDouble = 4,
        [Description("Triple")]
        Triple = 5,
        [Description("Quad")]
        Quad = 6,
        [Description("Family")]
        Family = 7,
        [Description("Suit")]
        Suit = 8,
        [Description("Studio")]
        Studio = 9,
        [Description("Apartment")]
        Apartment = 10,
    }
}

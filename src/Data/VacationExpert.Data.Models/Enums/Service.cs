namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum Service
    {
        [Display(Name="Free Wifi")]
        FreeWifi = 1,
        [Display(Name = "Non-smoking rooms")]
        NonSmokingRooms = 2,
        Restaurant = 3,
        [Display(Name = "Airport shuttle")]
        Airportshuttle = 4,
        [Display(Name = "Room service")]
        RoomService = 5,
        [Display(Name = "Family rooms")]
        FamilyRooms = 6,
        Bar = 7,
        Spa = 8,
        [Display(Name = "24-hour front desk")]
        TwentyFourHourFrontDesk = 9,
        [Display(Name = "Hot tub/Jacuzzi")]
        Hottubjacuzzi = 10,
        Sauna = 11,
        [Display(Name = "Air Conditioning")]
        AurConditioning = 12,
        [Display(Name = "Fitness Center")]
        FitnesCenter = 13,
        [Display(Name = "Water Park")]
        WaterPark = 14,
        Garden = 15,
        [Display(Name = "Swimming Pool")]
        SwimmingPool = 16,
        Terrace = 17,
    }
}

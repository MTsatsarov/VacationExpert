namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;

    public enum Service
    {
        [Description("Free Wifi")]
        FreeWifi = 1,
        [Description("Non-smoking rooms")]
        NonSmokingRooms = 2,
        Restaurant = 3,
        [Description("Airport shuttle")]
        Airportshuttle = 4,
        [Description("Room service")]
        RoomService = 5,
        [Description("Family rooms")]
        FamilyRooms = 6,
        Bar = 7,
        Spa = 8,
        [Description("24-hour front desk")]
        TwentyFourHourFrontDesk = 9,
        [Description("Hot tub/Jacuzzi")]
        Hottubjacuzzi = 10,
        Sauna = 11,
        [Description("Air Conditioning")]
        AurConditioning = 12,
        [Description("Fitness Center")]
        FitnesCenter = 13,
        [Description("Water Park")]
        WaterPark = 14,
        Garden = 15,
        [Description("Swimming Pool")]
        SwimmingPool = 16,
        Terrace = 17,
    }
}

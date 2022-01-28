namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;

    public enum Parking
    {
        No = 1,
        [Description("Yes,Paid")]
        YesPaid = 2,
        [Description("Yes,free")]
        Yesfree = 3,
    }
}

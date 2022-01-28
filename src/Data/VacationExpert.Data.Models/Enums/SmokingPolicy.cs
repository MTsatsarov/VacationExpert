namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;

    public enum SmokingPolicy
    {
        [Description("Non Smoking")]
        NonSmoking = 1,
        Smoking = 2,
        Both = 3,
    }
}

namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum SmokingPolicy
    {
        [Description("Non Smoking")]
        NonSmoking = 1,
        [Description("Smoking")]
        Smoking = 2,
        [Description("Both")]
        Both = 3,
    }
}

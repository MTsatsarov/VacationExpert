namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum SmokingPolicy
    {
        [Display(Name = "Non Smoking")]
        NonSmoking = 1,
        Smoking = 2,
        Both = 3,
    }
}

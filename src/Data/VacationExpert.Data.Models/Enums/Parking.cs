namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum Parking
    {
        No = 1,
        [Display(Name = "Yes,Paid")]
        YesPaid = 2,
        [Display(Name = "Yes,free")]
        Yesfree = 3,
    }
}

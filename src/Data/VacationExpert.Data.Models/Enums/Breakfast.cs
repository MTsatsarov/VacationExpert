namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum Breakfast
    {
        No = 1,
        [Display(Name = "Yes,Included in the price")]
        YesIncludedInThePrice = 2,
        [Display(Name = "Yes, it's optional")]
        YesItsOptional = 3,
    }
}

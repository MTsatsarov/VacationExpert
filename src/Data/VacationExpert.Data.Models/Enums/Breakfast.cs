namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;

    public enum Breakfast
    {
        No = 1,
        [Description("Yes,Included in the price")]
        YesIncludedInThePrice = 2,
        [Description("Yes, it's optional")]
        YesItsOptional = 3,
    }
}

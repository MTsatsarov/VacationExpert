namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum BedType
    {
        [Display(Name = "Twin bed(s) / 90-130 cm wide")]
        TwinBeds90130cmwide = 1,
        [Display(Name = "Full bed(s) / 131 - 150 cm wide")]
        FullBeds131150cmwide = 2,
        [Display(Name = "Queen bed(s) / 151 - 180 cm wide")]
        QueenBeds151180cmwide = 3,
        [Display(Name = "King bed(s) / 181 - 210 cm wide")]
        KingBeds181210cmwide = 4,
    }
}

namespace VacationExpert.Data.Models.Enums
{
    using System.ComponentModel;

    public enum BedType
    {
        [Description("Twin bed(s) / 90-130 cm wide")]
        TwinBeds90130cmwide = 1,
        [Description("Full bed(s) / 131 - 150 cm wide")]
        FullBeds131150cmwide = 2,
        [Description("Queen bed(s) / 151 - 180 cm wide")]
        QueenBeds151180cmwide = 3,
        [Description("King bed(s) / 181 - 210 cm wide")]
        KingBeds181210cmwide = 4,
    }
}

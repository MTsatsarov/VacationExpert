namespace VacationExpert.Data.Models
{
    using VacationExpert.Data.Common.Models;
    using VacationExpert.Data.Models.Enums;

    public class Bed : BaseDeletableModel<int>
    {
        public string Type { get; set; }

        public int Count { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}

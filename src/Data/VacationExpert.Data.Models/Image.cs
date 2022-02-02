namespace VacationExpert.Data.Models
{
    using System;

    using VacationExpert.Data.Common.Models;

    public class Image : BaseDeletableModel<Guid>
    {
        public Image()
        {
            this.Id = Guid.NewGuid();
        }

        public string OriginalFileName { get; set; }

        public string OriginalType { get; set; }

        public byte[] OriginalContent { get; set; }

        public byte[] ThumbnailContent { get; set; }

        public byte[] FullScreenContent { get; set; }

        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }
    }
}

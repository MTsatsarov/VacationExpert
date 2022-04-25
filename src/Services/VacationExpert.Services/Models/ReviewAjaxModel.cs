namespace VacationExpert.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Newtonsoft.Json;

    public class ReviewAjaxModel
    {
        [JsonProperty("propertyId")]
        public string PropertyId { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }
    }
}

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

namespace VacationExpert.Services.Models
{
    public class ReviewAjaxModel
    {
        [JsonProperty("propertyId")]
        public string PropertyId { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }
    }
}

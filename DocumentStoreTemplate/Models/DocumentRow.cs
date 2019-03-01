using System;
using Newtonsoft.Json;

namespace DocumentStoreTemplate.Models
{
    public class DocumentRow
    {
        [JsonIgnore]
        public int DocumentRowId { get; set; }

        [JsonProperty(propertyName: "Example Date")]
        public string ExampleDate { get; set; }
        public string ExampleString { get; set; }

        [JsonIgnore]
        public int DocumentId { get; set; }
        [JsonIgnore]
        public Document Document { get; set; }
    }
}
using System.Collections.Generic;

namespace DocumentStoreApi.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int Version { get; set; }
        public List<DocumentRow> Rows { get; set; }
    }
}
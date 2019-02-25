using System;

namespace DocumentStoreTemplate.Models
{
    public class DocumentRow
    {
        public int DocumentRowId { get; set; }

        public string ExampleString { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
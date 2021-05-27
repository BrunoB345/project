using System.Collections.Generic;

namespace SmartSpreadsheetImporterBackEnd.Services.JSONResponses
{
    public class JSONUpdateCheck
    {   
        public string Result { get; set; }
        public int Total { get; set; }
        public string Schema { get; set; }
        public List<Content> content { get; set; }

        public class Content
        {
            public ContentDesc schemaDesc { get; set; }

            public class ContentDesc
            {
                public string scrId { get; set; }
                public string scrLang { get; set; }
                public string scrChannel { get; set; }
                public string scrState { get; set; }
            }
        }
    }
}

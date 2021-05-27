using System;
using System.Collections.Generic;

namespace SmartSpreadsheetImporterBackEnd.Services
{
    public class JSONResponseChannels
    {
        public string Result { get; set; }
        public Channel ChannelData { get; set; }

        public class Channel
        {
            public string Id { get; set; }
            public string ParentId { get; set; }
            public string ParentName { get; set; }
            public string Name { get; set; }
            public string Desc { get; set; }
            public string Path { get; set; }
            public string BackofficeLayout { get; set; }
            public Children[] Descendents { get; set; }

            public class Children
            {
                public string Id { get; set; }
                public string Name { get; set; }
                public string Desc { get; set; }
                public string Path { get; set; }
                public string BackofficeLayout { get; set; }
                public Children[] Descendents { get; set; }
            }
        }
    }

    public class DataChannels
    {
        public DataChild dataChild { get; set; }
        public List<DataChild> dropChannels { get; set; }
    }

    public class DataChild
    {
        public string title { get; set; }
        public string value { get; set; }
        public Boolean canInsert { get; set; }
        public List<DataChild> children { get; set; }
    }
}

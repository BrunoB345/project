using System;
using System.Collections.Generic;

namespace SmartSpreadsheetImporterBackEnd.Services
{
    public class JSONResponseSchemas
    {
        public string Result { get; set; }
        public List<Schema> schemalist { get; set; }

        public class Schema
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public List<Channel> Channels { get; set; }

            public class Channel
            {
                public string Id { get; set; }
                public string Name { get; set; }
                public string Path { get; set; }
            }
        }
    }

    public class SchemaList
    {
        public List<SchemaRes> Schemas { get; set; }

    }
    public class SchemaRes{
            public string name{ get; set; }
            public string id{ get; set; }
        }
}

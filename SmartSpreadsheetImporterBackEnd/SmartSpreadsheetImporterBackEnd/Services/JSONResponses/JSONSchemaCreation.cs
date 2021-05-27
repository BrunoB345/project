using System.Collections.Generic;
using System.Data;

namespace SmartSpreadsheetImporterBackEnd.Services.JSONResponses
{
    public class JSONSchemaCreation
    {
        public string schemaName { get; set; }        
        public string schemaID { get; set; }
        public string channelName { get; set; }
        public string parentID { get; set; }
        public List<string> fieldNames { get; set; }
        public List<string> fieldTypes { get; set; }
        public DataTable table { get; set; }
        public int index { get; set; }
        public string[] dropdownPathsValue { get; set; }
        public string[] newDropdownSchemaValue { get; set; }
        public string[] newDropdownExistentValue { get; set; }
    }
}

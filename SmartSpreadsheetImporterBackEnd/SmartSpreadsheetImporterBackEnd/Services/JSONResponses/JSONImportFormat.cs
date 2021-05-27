using System.Collections.Generic;
using System.Data;

namespace SmartSpreadsheetImporterBackEnd.Services.JSONResponses
{
    public class JSONImportFormat
    {
        public string channelID { get; set; }
        public string[] scriptorRESTFieldsName { get; set; }
        public DataTable table { get; set; }
        public string[] excelNewFields { get; set; }
        public string[] excelFields { get; set; }
        public string[] fieldTypes { get; set; }
        public string[] dropdownPathsValue { get; set; }
        public string[] newDropdownSchemaValue { get; set; }
        public List<string> contChannelIDs { get; set; }
        public string[] scriptorMatching { get; set; }
        public bool newChannel { get; set; }
        public int[] matchingOrder { get; set; }
        public int index { get; set; }
        public string[] keys {get; set;}
        public string token { get; set; }
    }
}

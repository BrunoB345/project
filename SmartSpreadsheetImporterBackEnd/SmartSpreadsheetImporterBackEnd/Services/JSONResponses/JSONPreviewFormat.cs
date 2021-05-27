using System.Collections.Generic;
using System.Data;

namespace SmartSpreadsheetImporterBackEnd.Services.JSONResponses
{
    public class JSONPreviewFormat
    {
        public List<string> scriptorDropChannels { get; set; }
        public string[] scriptorFields { get; set; }
        public int[] scriptorFieldsOptional { get; set; }
        public string[] scriptorRESTFieldsName { get; set; }
        public string[] scriptorFieldsType { get; set; }
        public string[] excelFields { get; set; }
        public int numRows { get; set; }
        public int[] excelMatchingOrder { get; set; }
        public DataTable table { get; set; }
        public DataTable preview { get; set; }
    }
}

using System.Data;

namespace SmartSpreadsheetImporterBackEnd.Services.JSONResponses
{
    public class JSONCheckClientOptions
    {
        public string channelID { get; set; }
        public int[] scriptorFieldsOptional { get; set; }
        public string[] scriptorFieldsType { get; set; }
        public string[] excelFields { get; set; }
        public int[] excelMatchingOrder { get; set; }
        public DataTable table { get; set; }
    }
}

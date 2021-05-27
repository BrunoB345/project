using System.Collections.Generic;

namespace SmartSpreadsheetImporterBackEnd.Services.JSONResponses
{
    public class JSONImportProcessing
    {
        public List<string> contChannelIDs;
        public string Result { get; set; }
        public List<string> Status { get; set; }
        public List<string> Title { get; set; }
        public string token { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSpreadsheetImporterBackEnd.Services.JSONResponses
{
    public class JSONNewSchemaResponse
    {
        public string channelID { get; set; }
        public List<string> contChannelIDs { get; set; }
    }
}

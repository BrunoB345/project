using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSpreadsheetImporterBackEnd.Services
{
    public class JSONResponseToken
    {
        public string Result { get; set; }
        public string exists { get; set; }
        public string token { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }
}

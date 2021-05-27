using Newtonsoft.Json;
using SmartSpreadsheetImporterBackEnd.Services.JSONResponses;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;

namespace SmartSpreadsheetImporterBackEnd.Services
{
    public class Preview : PreviewInterface
    {

        public JSONImportReady checkTypes(JSONCheckClientOptions clientOptions)
        {
            DataTable table = clientOptions.table;
            string[] types = clientOptions.scriptorFieldsType;
            JSONImportReady result = new JSONImportReady();
            int[] order = clientOptions.excelMatchingOrder;
            result.response = true;
            int i = 0;
            int j = 0;
            foreach (string colName in clientOptions.excelFields)
            {
                if (order[j] >= 0)
                {
                    if (table.Rows[i][colName] != null && !table.Rows[i][colName].ToString().Equals("") && !table.Rows[i][colName].GetType().ToString().Equals("System.DBNull"))
                    {
                        var aux = table.Rows[i][colName].GetType();
                        if ((types[order[j]].Equals("integer") || types[order[j]].Equals("autonumber")) && (aux.Equals(typeof(long)) || aux.Equals(typeof(string))))
                            result.response = true;
                        else if ((types[order[j]].Equals("textline") || types[order[j]].Equals("dropdownlist") || types[order[j]].Equals("text") || types[order[j]].Equals("date") || types[order[j]].Equals("datetime")
                            || types[order[j]].Equals("title") || types[order[j]].Equals("url")) && aux.Equals(typeof(string)))
                            result.response = true;
                        else if (types[order[j]].Equals("real") && (aux.Equals(typeof(long)) || aux.Equals(typeof(double)) || aux.Equals(typeof(string))))
                            result.response = true;
                        else
                        {
                            result.response = false;
                            return result;
                        }
                    }
                }
                j++;
            }

            string channelID = clientOptions.channelID;
            Home home = new Home();
            string token = home.getToken();
            return result;
        }
    }
}

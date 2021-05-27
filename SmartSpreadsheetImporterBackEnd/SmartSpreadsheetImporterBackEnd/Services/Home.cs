using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Data.OleDb;
using static SmartSpreadsheetImporterBackEnd.Services.JSONResponseChannels.Channel;
using SmartSpreadsheetImporterBackEnd.Services.JSONResponses;
using static SmartSpreadsheetImporterBackEnd.Services.JSONResponseChannels;
using static SmartSpreadsheetImporterBackEnd.Services.JSONResponses.JSONResponseFields;
using System.Linq;
using System.Globalization;
using System;

namespace SmartSpreadsheetImporterBackEnd.Services
{
    public class Home : HomeInterface
    {
        private static string user = "bbrito";
        private static string pwd = "G8dxylu8";
        private string uriToken = "https://bo-estagios.scriptorserver.com/REST/validateUser?scriptorusername=" + user + "&scriptorpwd=" + pwd;
        private string uriChannels = "https://bo-estagios.scriptorserver.com/REST/channelList?authtoken=";
        private string uriFieldList = "https://bo-estagios.scriptorserver.com/REST/fieldList?authtoken=";
        
        private string uriSchemasList = "https://bo-estagios.scriptorserver.com/REST/schemalist?authtoken=";
        private string[] formats = { "dd/MM/yyyy","MM/dd/yyyy", "dd/MM/yy", "MM/dd/yy",
                                                    "dd/M/yyyy","d/MM/yyyy","d/M/yyyy","dd/M/yy","d/MM/yy","d/M/yy",
                                                    "M/dd/yyyy","MM/d/yyyy","M/d/yyyy","MM/d/yy","M/dd/yy","M/d/yy",
                                                    "dd/MM/yyyy HH:mm","MM/dd/yyyy HH:mm", "dd/MM/yy HH:mm", "MM/dd/yy HH:mm",
                                                    "dd/M/yyyy HH:mm","d/MM/yyyy HH:mm","d/M/yyyy HH:mm","dd/M/yy HH:mm","d/MM/yy HH:mm","d/M/yy HH:mm",
                                                    "M/dd/yyyy HH:mm","MM/d/yyyy HH:mm","M/d/yyyy HH:mm","MM/d/yy HH:mm","M/dd/yy HH:mm","M/d/yy HH:mm",
                                                    "dd/MM/yyyy H:mm","MM/dd/yyyy H:mm", "dd/MM/yy H:mm", "MM/dd/yy H:mm",
                                                    "dd/M/yyyy H:mm","d/MM/yyyy H:mm","d/M/yyyy H:mm","dd/M/yy H:mm","d/MM/yy H:mm","d/M/yy H:mm",
                                                    "M/dd/yyyy H:mm","MM/d/yyyy H:mm","M/d/yyyy H:mm","MM/d/yy H:mm","M/dd/yy H:mm","M/d/yy H:mm",
                                                    "dd/MM/yyyy HH:mm:ss","MM/dd/yyyy HH:mm:ss", "dd/MM/yy HH:mm:ss", "MM/dd/yy HH:mm:ss",
                                                    "dd/M/yyyy HH:mm:ss","d/MM/yyyy HH:mm:ss","d/M/yyyy HH:mm:ss","dd/M/yy HH:mm:ss","d/MM/yy HH:mm:ss","d/M/yy HH:mm:ss",
                                                    "M/dd/yyyy HH:mm:ss","MM/d/yyyy HH:mm:ss","M/d/yyyy HH:mm:ss","MM/d/yy HH:mm:ss","M/dd/yy HH:mm:ss","M/d/yy HH:mm:ss",

                                                    "dd-MM-yyyy","MM-dd-yyyy", "dd-MM-yy", "MM-dd-yy",
                                                    "dd-M-yyyy","d-MM-yyyy","d-M-yyyy","dd-M-yy","d-MM-yy","d-M-yy",
                                                    "M-dd-yyyy","MM-d-yyyy","M-d-yyyy","MM-d-yy","M-dd-yy","M-d-yy",
                                                    "dd-MM-yyyy HH:mm","MM-dd-yyyy HH:mm", "dd-MM-yy HH:mm", "MM-dd-yy HH:mm",
                                                    "dd-M-yyyy HH:mm","d-MM-yyyy HH:mm","d-M-yyyy HH:mm","dd-M-yy HH:mm","d-MM-yy HH:mm","d-M-yy HH:mm",
                                                    "M-dd-yyyy HH:mm","MM-d-yyyy HH:mm","M-d-yyyy HH:mm","MM-d-yy HH:mm","M-dd-yy HH:mm","M-d-yy HH:mm",
                                                    "dd-MM-yyyy H:mm","MM-dd-yyyy H:mm", "dd-MM-yy H:mm", "MM-dd-yy H:mm",
                                                    "dd-M-yyyy H:mm","d-MM-yyyy H:mm","d-M-yyyy H:mm","dd-M-yy H:mm","d-MM-yy H:mm","d-M-yy H:mm",
                                                    "M-dd-yyyy H:mm","MM-d-yyyy H:mm","M-d-yyyy H:mm","MM-d-yy H:mm","M-dd-yy H:mm","M-d-yy H:mm",
                                                    "dd-MM-yyyy HH:mm:ss","MM-dd-yyyy HH:mm:ss", "dd-MM-yy HH:mm:ss", "MM-dd-yy HH:mm:ss",
                                                    "dd-M-yyyy HH:mm:ss","d-MM-yyyy HH:mm:ss","d-M-yyyy HH:mm:ss","dd-M-yy HH:mm:ss","d-MM-yy HH:mm:ss","d-M-yy HH:mm:ss",
                                                    "M-dd-yyyy HH:mm:ss","MM-d-yyyy HH:mm:ss","M-d-yyyyTHH:mm:ss","MM-d-yy HH:mm:ss","M-dd-yy HH:mm:ss","M-d-yy HH:mm:ss"
                                };

        public DataChannels getChannels()
        {
            DataChild result;
            DataChannels auxResult = new DataChannels();
            List<DataChild> dropChannels = new List<DataChild>();
            JSONResponseChannels jsonReply;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriChannels + getToken() + "&channelPath=/");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonReply = JsonConvert.DeserializeObject<JSONResponseChannels>(reader.ReadToEnd());
            }

            result = new DataChild();
            Channel root = jsonReply.ChannelData;
            result.title = root.Name;
            result.canInsert = int.Parse(root.BackofficeLayout) < 2;
            result.value = root.Id;
            if (result.canInsert)
            {
                DataChild auxChild = new DataChild();
                auxChild.title = result.title;
                auxChild.value = result.value;
                dropChannels.Add(auxChild);
            }
            List<DataChild> aux = new List<DataChild>();
            result.children = aux;
            if (root.Descendents != null)
            {
                foreach (Children descendentData in root.Descendents)
                {
                    aux.Add(getChildrenNames(descendentData, dropChannels));
                }
            }
            auxResult.dataChild = result;
            auxResult.dropChannels = dropChannels;
            return auxResult;
        }

        private DataChild getChildrenNames(Children parent, List<DataChild> dropChannels)
        {
            DataChild result = new DataChild();
            result.title = parent.Name;
            result.canInsert = int.Parse(parent.BackofficeLayout) < 2;
            result.value = parent.Id;
            if (result.canInsert)
            {
                DataChild auxChild = new DataChild();
                auxChild.title = result.title;
                auxChild.value = result.value;
                dropChannels.Add(auxChild);
            }
            if (parent.Descendents != null)
            {
                List<DataChild> aux = new List<DataChild>();
                result.children = aux;
                foreach (Children descendentData in parent.Descendents)
                {
                    aux.Add(getChildrenNames(descendentData, dropChannels));
                }
            }
            return result;
        }

        public string getToken()
        {
            JSONResponseToken result;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriToken);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = JsonConvert.DeserializeObject<JSONResponseToken>(reader.ReadToEnd());
            }
            return result.token;
        }

        public JSONPreviewFormat getPreviewOfNewChannel(string filePath, string sheet)
        {
            DataTable table = getTable(filePath, sheet);
            var ExcelList = new List<string>();
            JSONPreviewFormat result = new JSONPreviewFormat();
            result.scriptorFieldsType = new string[table.Columns.Count];
            int i = 0;
            DataRow firstRow = table.Rows[0];
            foreach (DataColumn column in table.Columns)
            {
                var values = table.Rows.Cast<DataRow>()
                        .Select(r => r[column])
                        .Distinct()
                        .ToList();
                System.DateTime b;
                var date = System.DateTime.TryParseExact("" + firstRow[i], formats, CultureInfo.InvariantCulture, DateTimeStyles.None,out b);
                var unique = values.Count <= table.Rows.Count/5;
                if((firstRow[i].ToString().Equals("") || firstRow[i].GetType().ToString().Equals("System.DBNull")) && !unique)
                    result.scriptorFieldsType[i] = "textline";
                else if(unique && (firstRow.ItemArray[i].GetType().ToString().Equals("System.Double")|| firstRow.ItemArray[i].GetType().ToString().Equals("System.Int64")))
                    result.scriptorFieldsType[i] = "textline";
                else if(date)
                    result.scriptorFieldsType[i] = "date";
                else if (unique)
                    result.scriptorFieldsType[i]= "dropdownlist";
                else
                    result.scriptorFieldsType[i]= "textline";
                i++;
                ExcelList.Add(column.ColumnName);
            }
            
            result.excelFields = ExcelList.ToArray();
            result.numRows = table.Rows.Count;
            DataTable aux = new DataTable();
            foreach (DataColumn collumn in table.Columns) {
                aux.Columns.Add(collumn.ColumnName);
            }
            for(int j = 0; j < 100 && j < table.Rows.Count; j++)
            {
                aux.Rows.Add(table.Rows[j].ItemArray);
            }
            result.table = table;
            result.preview = aux;
            return result;
        }

        public JSONPreviewFormat mapExcel(string filePath, string channelID, string sheet)
        {
            JSONResponseFields fields = getSchemaFields(channelID);
            var ScriptorList = new List<string>();
            var ScriptorListRESTNames = new List<string>();
            var ScriptorListTypes = new List<string>();
            var ScriptorListDropChannels = new List<string>();
            var ScriptorFieldsOptional = new List<int>();
            var test = fields.fieldList;
            DataTable table = getTable(filePath, sheet);
            foreach (Field f in test)
            {
                if (!f.fk_field_type.Equals("autonumber"))
                {
                    ScriptorList.Add(f.txt_prompt);
                    ScriptorListRESTNames.Add(f.txt_name);
                    ScriptorListTypes.Add(f.fk_field_type);
                    ScriptorFieldsOptional.Add(int.Parse(f.b_optional));
                }
                if (f.fk_field_type.Equals("dropdownlist"))
                {
                    if(f.fk_content_channel.Equals(channelID))
                    {
                        ScriptorListDropChannels.Add("self");
                    }
                    else
                    {
                        ScriptorListDropChannels.Add(f.fk_content_channel);
                    }
                }
                else
                {
                    ScriptorListDropChannels.Add("null");
                }
            }

            var ExcelList = new List<string>();
            var ExcelColums = new List<string>();
            foreach (DataColumn column in table.Columns)
            {
                ExcelList.Add(column.ColumnName);
                ExcelColums.Add(column.ColumnName);
            }
            if (ScriptorList.Count > ExcelList.Count)
                return null;
            var NewList = new List<int>();
            for (int j = 0; j < ScriptorList.Count; j++)
            {
                int matchingIndex = 0;
                int min = int.MaxValue;
                for (int i = 0; i < ExcelList.Count; i++)
                {
                    int current = LDAlgorithm(ScriptorList[j], ExcelList[i]);
                    if (current < min)
                    {
                        min = current;
                        matchingIndex = i;
                    }
                }
                NewList.Add(ExcelColums.IndexOf(ExcelList[matchingIndex]));
                ExcelList.RemoveAt(matchingIndex);
            }

            JSONPreviewFormat result = new JSONPreviewFormat();
            ScriptorList.Add("New Field");
            ScriptorList.Add("Don't Import");
            result.scriptorFields = ScriptorList.ToArray();
            result.excelFields = ExcelColums.ToArray();
            result.excelMatchingOrder = NewList.ToArray();
            result.scriptorFieldsType = ScriptorListTypes.ToArray();
            result.scriptorRESTFieldsName = ScriptorListRESTNames.ToArray();
            result.scriptorFieldsOptional = ScriptorFieldsOptional.ToArray();
            result.numRows = table.Rows.Count;
            result.scriptorDropChannels = ScriptorListDropChannels;
            DataTable aux = new DataTable();
            foreach (DataColumn collumn in table.Columns)
            {
                aux.Columns.Add(collumn.ColumnName);
            }
            for (int j = 0; j < 100 && j < table.Rows.Count; j++)
            {
                aux.Rows.Add(table.Rows[j].ItemArray);
            }
            result.table = table;
            result.preview = aux;
            return result;
        }

        private DataTable getTable(string filePath, string sheet)
        {
            string con = "";
            if (!filePath.Contains(".xlsx"))
            {
                con = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + filePath + ";Extended Properties='Excel 8.0;HDR=YES;'";
            }
            else
            {
                con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=YES;'";
            }
            DataTable table = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                OleDbCommand command = new OleDbCommand("select * from [" + sheet + "]", connection);
                using (OleDbDataAdapter da = new OleDbDataAdapter())
                {
                    da.SelectCommand = command;
                    da.Fill(table);
                    connection.Close();
                }
            }
            return table;
        }

        private static int LDAlgorithm(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            int cost;

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    cost = (t.Substring(j - 1, 1) == s.Substring(i - 1, 1) ? 0 : 1);

                    d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                  d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        public JSONResponseFields getSchemaFields(string channelID)
        {
            JSONResponseFields result;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriFieldList + getToken() + "&channelID=" + channelID);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = JsonConvert.DeserializeObject<JSONResponseFields>(reader.ReadToEnd());
            }

            return result;
        }

        public SchemaList getSchemas()
        {
            JSONResponseSchemas jsonReply;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriSchemasList + getToken() + "&channelPath=/");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonReply = JsonConvert.DeserializeObject<JSONResponseSchemas>(reader.ReadToEnd());
            }
            SchemaList schemaList = new SchemaList();
            var schemas = new List<SchemaRes>();
            foreach(var schema in jsonReply.schemalist){
                SchemaRes schema1 = new SchemaRes();
                schema1.name = schema.Name;
                schema1.id = schema.Id;
                schemas.Add(schema1);
            }
            schemaList.Schemas = schemas;
            return schemaList;
        }
    }
}

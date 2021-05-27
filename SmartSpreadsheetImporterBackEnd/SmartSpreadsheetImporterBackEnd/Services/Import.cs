using Newtonsoft.Json;
using SmartSpreadsheetImporterBackEnd.Services.JSONResponses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;

namespace SmartSpreadsheetImporterBackEnd.Services
{
    public class Import : ImportInterface
    {
        private string uriSchema = "https://bo-estagios.scriptorserver.com/REST/saveschema?authtoken=";
        private string uriChannel = "https://bo-estagios.scriptorserver.com/REST/savechannel?authtoken=";
        private string uriContent = "https://bo-estagios.scriptorserver.com/REST/saveContent?authtoken=";
        private string uriField = "https://bo-estagios.scriptorserver.com/REST/saveschemafield?authtoken=";
        private string urilistCond = "https://bo-estagios.scriptorserver.com/REST/contentlistconditional?authtoken=";
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


        public JSONResponseImport import(string uri)
        {
            JSONResponseImport result;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = JsonConvert.DeserializeObject<JSONResponseImport>(reader.ReadToEnd());
            }
            return result;
        }

        public string createSchema(JSONSchemaCreation importInfo, string token)
        {
            string uri = uriSchema + token + "&txt_name=" + importInfo.schemaName + "&log_content=0";
            JSONSchema result;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = JsonConvert.DeserializeObject<JSONSchema>(reader.ReadToEnd());
            }
            return result.SchemaID;
        }

        public string createChannel(JSONSchemaCreation importInfo, string schemaID, string workflowID, string token)
        {
            string uri = uriChannel + token + "&parent_channel_id=" + importInfo.parentID + "&txt_name=" + importInfo.channelName + "&schemaID=" + schemaID + "&workflowID=" + workflowID;
            JSONChannel result;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = JsonConvert.DeserializeObject<JSONChannel>(reader.ReadToEnd());
            }
            return result.channelID;
        }

        public string addFields(string schemaID, JSONSchemaCreation importInfo, List<string> contInsSchemas, List<string> contInsChannels, string token)
        {
            string result = "";
            int i = 0;
            string uri = uriField + token + "&schemaID=" + schemaID + "&n_backofficelayout=1";
            string uriInserts = uriField + token + "&schemaID=";
            string aux = "";
            HttpWebRequest request;

            while (i < importInfo.fieldNames.Count)
            {
                if (importInfo.fieldTypes[i].Equals("dropdownlist"))
                {
                    if (!contInsSchemas[i].Equals("undefined"))
                    {
                        aux = uriInserts + contInsSchemas[i] + "&txt_name=" + importInfo.fieldNames[i].Replace(' ', '_').Replace('/', '_') + "&txt_prompt=" + importInfo.fieldNames[i] + "&b_unique=1&fk_field_type=title&n_db_size=900&b_optional=1&n_backofficelayout=1";
                        request = (HttpWebRequest)WebRequest.Create(aux);
                        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string res = reader.ReadToEnd();
                        }

                        aux = uri + "&txt_name=" + importInfo.fieldNames[i].Replace(' ', '_').Replace('/', '_') + "&txt_prompt=" + importInfo.fieldNames[i] + "&fk_field_type=" + importInfo.fieldTypes[i] + "&n_db_size=900" + "&b_optional=1" + "&fk_content_schema=" + contInsSchemas[i] + "&fk_content_channel=" + contInsChannels[i];
                        request = (HttpWebRequest)WebRequest.Create(aux);
                        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string res = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        aux = uri + "&txt_name=" + importInfo.fieldNames[i].Replace(' ', '_') + "&txt_prompt=" + importInfo.fieldNames[i] + "&fk_field_type=" + importInfo.fieldTypes[i] + "&n_db_size=900" + "&b_optional=1" + "&fk_content_channel=" + contInsChannels[i];
                        request = (HttpWebRequest)WebRequest.Create(aux);
                        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string res = reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    aux = uri + "&txt_name=" + importInfo.fieldNames[i].Replace(' ', '_') + "&txt_prompt=" 
                    + importInfo.fieldNames[i] + "&fk_field_type=" + importInfo.fieldTypes[i] + "&n_db_size=900&b_optional=1";
                    request = (HttpWebRequest)WebRequest.Create(aux);
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string res = reader.ReadToEnd();
                    }
                }
                i++;
            }
            return result;
        }

        public JSONNewSchemaResponse checkContentInserts(JSONSchemaCreation importInfo, string token)
        {
            string schemaID = importInfo.schemaID;
            if(schemaID == "")
                schemaID = createSchema(importInfo, token);
            string channelID = createChannel(importInfo, schemaID, "b3ed974c-9036-4f73-a3e4-ebc10263294a", token);
            List<string> contInsChannels = new List<string>();
            List<string> contInsSchemas = new List<string>();
            int i = 0;
            JSONSchemaCreation contAux = new JSONSchemaCreation();
            foreach (string type in importInfo.fieldTypes)
            {
                if (type.Equals("dropdownlist") && importInfo.dropdownPathsValue[i].Equals("New child channel"))
                {
                    contAux.schemaName = importInfo.newDropdownSchemaValue[i].Replace(" ", "");
                    string schemaAux = createSchema(contAux, token);
                    contInsSchemas.Add(schemaAux);
                    contAux.parentID = channelID;
                    contAux.channelName = importInfo.newDropdownSchemaValue[i].Replace(" ", "");
                    string channelAux = createChannel(contAux, schemaAux, "b3ed974c-9036-4f73-a3e4-ebc10263294a", token);
                    contInsChannels.Add(channelAux);
                }
                else if (type.Equals("dropdownlist") && importInfo.dropdownPathsValue[i].Equals("Existent channel"))
                {
                    contInsSchemas.Add("undefined");
                    contInsChannels.Add(importInfo.newDropdownExistentValue[i]);
                }
                else
                {
                    contInsSchemas.Add("undefined");
                    contInsChannels.Add(channelID);
                }
                i++;
            }
            addFields(schemaID, importInfo, contInsSchemas, contInsChannels, token);
            JSONNewSchemaResponse result = new JSONNewSchemaResponse();
            result.channelID = channelID;
            result.contChannelIDs = contInsChannels;
            return result;
        }

        public JSONImportProcessing importContents(JSONImportFormat importInfo, bool newChannel, string token)
        {
            string channelID = importInfo.channelID;
            DataTable table = importInfo.table;
            string[] excelOrder = importInfo.excelFields;
            List<string> status = new List<string>();
            string[] keys = importInfo.keys;
            List<string> titles = new List<string>();
            string jsonReply = "";
            int[] matchingOrder = importInfo.matchingOrder;
            string[] fields = importInfo.scriptorRESTFieldsName;
            JSONImportProcessing result = new JSONImportProcessing();
            JSONUpdateCheck reply = new JSONUpdateCheck();
            int titlePosition = 0;
            bool isUpdate = false;
            for (int j = 0; j < importInfo.fieldTypes.Length; j++)
            {
                if (importInfo.fieldTypes[j] == "title")
                {
                    titlePosition = j;
                    break;
                }

            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriContent);
            for (int i = 0; i < importInfo.table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                string uriInsert = uriContent + token + "&channelID=" + channelID;
                string uri = "";
                if (!newChannel)
                {
                    for (int j = 0; j < importInfo.scriptorMatching.Length; j++)
                    {
                        if ((importInfo.scriptorMatching[j].Contains("dropdownlist") || 
                        (importInfo.fieldTypes[j] == "dropdownlist" && importInfo.matchingOrder[j] == -1)) 
                        && importInfo.contChannelIDs[j] != channelID && !row[importInfo.excelFields[j]].ToString().Equals("") 
                        && row[importInfo.excelFields[j]] != null)
                        {
                            string uriAux = "https://bo-estagios.scriptorserver.com/REST/saveContent?authtoken=" + token + "&channelID=" + importInfo.contChannelIDs[j] + "&" + importInfo.excelNewFields[j].Replace(" ", "_").Replace('/', '_') + "=" + row[importInfo.excelFields[j]];
                            request = (HttpWebRequest)WebRequest.Create(uriAux);
                            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                reader.ReadToEnd();
                            }
                        }
                        if ((importInfo.scriptorMatching[j].Contains("dropdownlist") || importInfo.fieldTypes[j].Equals("dropdownlist")) && importInfo.contChannelIDs[j] == channelID && !row[importInfo.excelFields[j]].ToString().Equals("") && row[importInfo.excelFields[j]] != null)
                        {
                            reply = new JSONUpdateCheck();
                            string dropCheck = urilistCond + token + "&channelID=" + channelID + "&condition=";
                            var id = row[importInfo.excelFields[j]];
                            dropCheck += importInfo.scriptorRESTFieldsName[titlePosition].Replace(' ', '_').Replace('/', '_') + ",equals," + id;
                            request = (HttpWebRequest)WebRequest.Create(dropCheck);
                            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                            jsonReply = "";
                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                jsonReply = reader.ReadToEnd();
                                reply = JsonConvert.DeserializeObject<JSONUpdateCheck>(jsonReply);
                            }
                            if (reply.Total == 0)
                            {
                                string uriAux = "https://bo-estagios.scriptorserver.com/REST/saveContent?authtoken=" + token + "&channelID=" + channelID + "&" + importInfo.scriptorRESTFieldsName[titlePosition].Replace(' ', '_').Replace('/', '_') + "=" + row[importInfo.excelFields[j]];
                                request = (HttpWebRequest)WebRequest.Create(uriAux);
                                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                                
                                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                                using (Stream stream = response.GetResponseStream())
                                using (StreamReader reader = new StreamReader(stream))
                                {
                                    reader.ReadToEnd();
                                }
                            }
                        }
                    }

                    reply = new JSONUpdateCheck();
                    string var = urilistCond + token + "&channelID=" + channelID + "&condition=";
                    for (int k = 0; k < keys.Length; k++)
                    {
                        if (keys[k].Equals("accepted"))
                        {
                            var id = row[importInfo.excelFields[k]];
                            var += importInfo.scriptorRESTFieldsName[matchingOrder[k]] + ",equals," + id + "&";
                        }
                    }

                    request = (HttpWebRequest)WebRequest.Create(var);
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    jsonReply = "";
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        jsonReply = reader.ReadToEnd();
                        reply = JsonConvert.DeserializeObject<JSONUpdateCheck>(jsonReply);
                    }
                    if (reply.Total >= 1)
                    {
                        isUpdate = true;
                        uriInsert += "&contentID=" + getContentID(jsonReply, "scrId\": \"", "\",\"scrLang");
                    }

                    uri = uriInsert;
                    var index = 0;

                    foreach (string col in excelOrder)
                    {
                        string x = "";
                        if (importInfo.fieldTypes[index].Equals("date"))
                        {
                            
                            var date = DateTime.ParseExact("" + row[col], formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                            if (date.ToString().Contains(":"))
                            {
                                x = date.ToString("yyyy/MM/dd HH:mm:ss");
                            }
                            else
                            {
                                x = date.ToString("yyyy/MM/dd");
                            }
                        }
                        if (matchingOrder[index] < 0)
                        {
                            if (matchingOrder[index] == -1)
                            {
                                uri += "&" + importInfo.excelNewFields[index].Replace(' ', '_').Replace('/', '_') + "=";
                                if (!x.Equals(""))
                                {
                                    uri += HttpUtility.UrlEncode(x);
                                }
                                else
                                {

                                    uri += HttpUtility.UrlEncode("" + row[index]);
                                }
                            }
                        }
                        else
                        {
                            if (!row[col].ToString().Equals("") && row[col] != null)
                            {
                                if (importInfo.scriptorMatching[index].Contains("title"))
                                {
                                    titles.Add(row[col] + "");
                                }
                                uri += "&" + fields[matchingOrder[index]] + "=";
                                if (!x.Equals(""))
                                {
                                    uri += HttpUtility.UrlEncode(x);
                                }
                                else
                                {

                                    uri += HttpUtility.UrlEncode("" + row[col]);
                                }
                            }
                        }
                        index++;


                    }
                    JSONResponseImport res = import(uri);
                    result.Result = res.Result;
                    if (isUpdate)
                        status.Add("Updated");
                    else
                        status.Add("New content");
                    result.token = token;
                    result.contChannelIDs = importInfo.contChannelIDs;
                }
                else
                {
                    for (int j = 0; j < importInfo.fieldTypes.Length; j++)
                    {
                        if (importInfo.fieldTypes[j].Equals("dropdownlist") && importInfo.contChannelIDs[j] != channelID && !row[importInfo.excelFields[j]].ToString().Equals("") && row[importInfo.excelFields[j]] != null)
                        {
                            string uriAux = "https://bo-estagios.scriptorserver.com/REST/saveContent?authtoken=" + token + "&channelID=" + importInfo.contChannelIDs[j] + "&" + importInfo.scriptorRESTFieldsName[j].Replace(' ', '_').Replace('/', '_') + "=" + row[importInfo.excelFields[j]];
                            request = (HttpWebRequest)WebRequest.Create(uriAux);
                            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                reader.ReadToEnd();
                            }
                        }
                        if (importInfo.fieldTypes[j].Equals("dropdownlist") && importInfo.contChannelIDs[j] == channelID && !row[importInfo.excelFields[j]].ToString().Equals("") && row[importInfo.excelFields[j]] != null)
                        {
                            reply = new JSONUpdateCheck();
                            string dropCheck = urilistCond + token + "&channelID=" + channelID + "&condition=";
                            var id = row[importInfo.excelFields[j]];
                            dropCheck += importInfo.scriptorRESTFieldsName[titlePosition].Replace(' ', '_').Replace('/', '_') + ",equals," + id;
                            request = (HttpWebRequest)WebRequest.Create(dropCheck);
                            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                            jsonReply = "";
                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                jsonReply = reader.ReadToEnd();
                                reply = JsonConvert.DeserializeObject<JSONUpdateCheck>(jsonReply);
                            }
                            if (reply.Total == 0)
                            {
                                string uriAux = "https://bo-estagios.scriptorserver.com/REST/saveContent?authtoken=" + token + "&channelID=" + channelID + "&" + importInfo.scriptorRESTFieldsName[titlePosition].Replace(' ', '_').Replace('/', '_') + "=" + row[importInfo.excelFields[j]];
                                request = (HttpWebRequest)WebRequest.Create(uriAux);
                                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                                using (Stream stream = response.GetResponseStream())
                                using (StreamReader reader = new StreamReader(stream))
                                {
                                    reader.ReadToEnd();
                                }
                            }
                        }
                    }

                    uri = uriInsert;
                    var index = 0;
                    foreach (string col in excelOrder)
                    {
                        if (!row[col].ToString().Equals("") && row[col] != null)
                        {
                            if (importInfo.fieldTypes[index].Equals("title"))
                            {
                                titles.Add(row[col] + "");
                            }
                            uri += "&" + fields[index].Replace('/', '_').Replace(' ', '_') + "=";
                            if (importInfo.fieldTypes[index].Equals("date"))
                            {
                               
                                var date = DateTime.ParseExact("" + row[col], formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                                string x = "";
                                if (date.ToString().Contains(":"))
                                {
                                    x = date.ToString("yyyy/MM/dd HH:mm");
                                }
                                else
                                {
                                    x = date.ToString("yyyy/MM/dd");
                                }
                                uri += HttpUtility.UrlEncode(x);
                            }
                            else
                            {
                                uri += HttpUtility.UrlEncode("" + row[col]);
                            }
                        }
                        index++;
                    }
                    JSONResponseImport res = import(uri);
                    result.Result = res.Result;
                    status.Add("New content");
                    result.token = token;
                }
            }
            result.Status = status;
            result.Title = titles;
            return result;
        }

        private string getContentID(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
    }
}

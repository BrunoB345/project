using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SmartSpreadsheetImporterBackEnd.Services;
using SmartSpreadsheetImporterBackEnd.Services.JSONResponses;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartSpreadsheetImporterBackEnd.Controllers
{
    [EnableCors("Policy")]
    [Route("import")]
    [ApiController]
    public class ImportController : Controller
    {
        private readonly ImportInterface _services;
        private IHostingEnvironment _env;

        public ImportController(ImportInterface services, IHostingEnvironment env)
        {
            _services = services;
            _env = env;
        }

        [HttpPost]
        [Route("finish")]
        public ActionResult<JSONImportProcessing> importTable([FromBody]JSONImportFormat importInfo)
        {
            Home home = new Home();
            string token = "";
            if (importInfo.token == null || importInfo.token.Equals(""))
            {
                token = home.getToken();
            }
            else
            {
                token = importInfo.token;
            }
            int j = 0;
            JSONSchemaCreation newField = new JSONSchemaCreation();
            List<string> names = new List<string>();
            List<string> types = new List<string>();
            List<string> contInsChannels = new List<string>();
            List<string> contInsSchemas = new List<string>();
            if (!importInfo.newChannel && importInfo.index == 0)
            {
                foreach (int i in importInfo.matchingOrder)
                {
                    if (i == -1)
                    {
                        if (importInfo.fieldTypes[j] == "dropdownlist")
                        {
                            if (importInfo.dropdownPathsValue[j].Equals("New child channel"))
                            {
                                JSONSchemaCreation contAux = new JSONSchemaCreation();
                                contAux.schemaName = importInfo.newDropdownSchemaValue[j].Replace(" ", "");
                                string schemaAux = _services.createSchema(contAux, token);
                                contInsSchemas.Add(schemaAux);
                                contAux.parentID = importInfo.channelID;
                                contAux.channelName = importInfo.newDropdownSchemaValue[j].Replace(" ", "");
                                string channelAux = _services.createChannel(contAux, schemaAux, "b3ed974c-9036-4f73-a3e4-ebc10263294a", token);
                                contInsChannels.Add(channelAux);
                                importInfo.contChannelIDs[j] = channelAux;
                            }
                            else if (importInfo.dropdownPathsValue[j].Equals("Existent channel"))
                            {
                                contInsSchemas.Add("undefined");
                                contInsChannels.Add(importInfo.contChannelIDs[j]);
                            }
                            else
                            {
                                contInsSchemas.Add("undefined");
                                contInsChannels.Add(importInfo.channelID);
                            }
                        }
                        names.Add(importInfo.excelNewFields[j]);
                        types.Add(importInfo.fieldTypes[j]);

                    }
                    j++;
                }
                if (names.Count > 0)
                {
                    string schemaID = home.getSchemaFields(importInfo.channelID).fieldList[0].schema_id;
                    newField.fieldTypes = types;
                    newField.fieldNames = names;
                    _services.addFields(schemaID, newField, contInsSchemas, contInsChannels, token);
                }
            }
            return _services.importContents(importInfo, importInfo.newChannel, token);
        }

        [HttpPost]
        [Route("schema")]
        public ActionResult<JSONImportFormat> createNewSchema([FromBody]JSONSchemaCreation importInfo)
        {
            Home home = new Home();
            string token = home.getToken();
            JSONNewSchemaResponse result = new JSONNewSchemaResponse();
            result = _services.checkContentInserts(importInfo, token);

            string channelID = result.channelID;
            JSONImportFormat import = new JSONImportFormat();
            import.channelID = channelID;
            import.contChannelIDs = result.contChannelIDs;
            import.table = importInfo.table;
            List<string> restNames = new List<string>();
            foreach (string f in importInfo.fieldNames)
            {
                restNames.Add(f.Replace(' ', '_'));
            }
            import.scriptorRESTFieldsName = restNames.ToArray();
            var ExcelList = new List<string>();
            foreach (DataColumn column in importInfo.table.Columns)
            {
                ExcelList.Add(column.ColumnName);
            }
            import.excelFields = ExcelList.ToArray();
            import.fieldTypes = importInfo.fieldTypes.ToArray();
            import.newChannel = true;
            import.index = 0;
            return import;
        }
    }
}

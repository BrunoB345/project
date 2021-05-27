using SmartSpreadsheetImporterBackEnd.Services.JSONResponses;
using System.Collections.Generic;

namespace SmartSpreadsheetImporterBackEnd.Services
{
    public interface ImportInterface
    {
        JSONImportProcessing importContents(JSONImportFormat importInfo, bool newChannel, string token);
        string createSchema(JSONSchemaCreation importInfo, string token);
        string addFields(string schemaID, JSONSchemaCreation importInfo, List<string> contInsSchemas, List<string> contInsChannels, string token);
        string createChannel(JSONSchemaCreation importInfo, string schemaID, string workflowID, string token);
        JSONNewSchemaResponse checkContentInserts(JSONSchemaCreation importInfo, string token);
        JSONResponseImport import(string uri);
    }
}

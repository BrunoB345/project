using SmartSpreadsheetImporterBackEnd.Services.JSONResponses;

namespace SmartSpreadsheetImporterBackEnd.Services
{
    public interface HomeInterface
    {
        DataChannels getChannels();
        SchemaList getSchemas();
        string getToken();
        JSONPreviewFormat getPreviewOfNewChannel(string filePath, string sheet);
        JSONPreviewFormat mapExcel(string filePath, string channelID, string sheet);
        JSONResponseFields getSchemaFields(string path);
    }
}

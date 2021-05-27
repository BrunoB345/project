using SmartSpreadsheetImporterBackEnd.Services.JSONResponses;
using System.Data;

namespace SmartSpreadsheetImporterBackEnd.Services
{
    public interface PreviewInterface
    {
        JSONImportReady checkTypes(JSONCheckClientOptions importFile);
    }
}

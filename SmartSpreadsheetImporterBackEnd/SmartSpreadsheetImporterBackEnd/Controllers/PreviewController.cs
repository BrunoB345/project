using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SmartSpreadsheetImporterBackEnd.Services;
using SmartSpreadsheetImporterBackEnd.Services.JSONResponses;

namespace SmartSpreadsheetImporterBackEnd.Controllers
{
    [EnableCors("Policy")]
    [Route("preview")]
    [ApiController]
    public class PreviewController : Controller
    {

        private readonly PreviewInterface _services;
        private IHostingEnvironment _env;

        public PreviewController(PreviewInterface services, IHostingEnvironment env)
        {
            _services = services;
            _env = env;
        }

        [HttpPost]
        [Route("check")]
        public ActionResult<JSONImportReady> checkAlterations([FromBody]JSONCheckClientOptions clientOptions)
        {
            return _services.checkTypes(clientOptions);            
        }
    }
}

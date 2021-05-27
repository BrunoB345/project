using System;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSpreadsheetImporterBackEnd.Services;
using SmartSpreadsheetImporterBackEnd.Services.JSONResponses;

namespace SmartSpreadsheetImporterBackEnd.Controllers
{
    [EnableCors("Policy")]
    [Route("home")]
    [ApiController]
    public class HomeController : Controller
    {

        private readonly HomeInterface _services;
        private IHostingEnvironment _env;

        public HomeController(HomeInterface services, IHostingEnvironment env)
        {
            _services = services;
            _env = env;
        }

        [HttpGet]
        [Route("schemas")]
        public ActionResult<SchemaList> GetListOfSchemas()
        {
            return _services.getSchemas();
        }

        [HttpPost]
        [Route("currSchema")]
        public ActionResult<string> GetCurrSchema(IFormFile file)
        {
            string channelID = HttpContext.Request.Form["channelID"];
            return _services.getSchemaFields(channelID).fieldList[0].schema_id;;
        }

        [HttpGet]
        [Route("channels")]
        public ActionResult<DataChannels> GetListOfChannels()
        {
            return _services.getChannels();
        }
        
        [HttpPost]
        [Route("preview")]
        public ActionResult<JSONPreviewFormat> GetExcelPreview(IFormFile file)
        {
            using (var fileStream = new FileStream(Path.Combine(_env.ContentRootPath, "file.xlsx"), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
            string sheet = HttpContext.Request.Form["sheet"] + "$";
            if (HttpContext.Request.Form["newChannel"].Equals("false"))
                return _services.mapExcel("./file.xlsx", HttpContext.Request.Form["channelID"], sheet);
            return _services.getPreviewOfNewChannel("./file.xlsx", sheet);
        }
    }
}
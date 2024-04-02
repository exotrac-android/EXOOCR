using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net;

namespace EXOOCR.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OCRController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IOCRService _OCRservice;

    public OCRController(
        IOCRService ocrService,
        IConfiguration config)
    {
        _OCRservice = ocrService;
        _config = config;
    }


    [HttpPost]
    public async Task<string> ReadImage([FromBody] string url)
    {
        try
        {
            var content = _OCRservice.GetImage(url);
            var httpClient = _OCRservice.GetHttpClient();

            HttpResponseMessage httpResp = await httpClient.PostAsync("computervision/imageanalysis:analyze/?features=caption,read&model-version=latest&language=en&api-version=2024-02-01", content);
            string responseData = await httpResp.Content.ReadAsStringAsync();
            return responseData;
        }
        catch (Exception ex)
        {
            return "Exception thrown";
        }
    }

}

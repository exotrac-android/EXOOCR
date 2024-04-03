using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
    public async Task<string> GetContainerNumber([FromBody] string url)
    {
        //url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFJS5144kuVJx2s2b_1XoXPIOF0TXNhTcCcFBIgFO5pA&s";
        try
        {
            // Get captured Image
            var content = _OCRservice.GetImage(url);
            
            // Call Azure AI Service to get all texts:
            var httpClient = _OCRservice.GetHttpClient();

            HttpResponseMessage httpResp = await httpClient.PostAsync("computervision/imageanalysis:analyze/?features=caption,read&model-version=latest&language=en&api-version=2024-02-01", content);
            string respObj = await httpResp.Content.ReadAsStringAsync();

            var ocrResponse = JObject.Parse(respObj).ToObject<OCRResp>();
            if (ocrResponse.ReadResult == null)
            {
                return "No Response";
            }

            // Filter Container Number using RegEx
            var containerNumber = _OCRservice.GetContainerNumber(ocrResponse.ReadResult.Blocks);
            return containerNumber;
        }
        catch (Exception ex)
        {
            return "Exception thrown";
        }
    }

}

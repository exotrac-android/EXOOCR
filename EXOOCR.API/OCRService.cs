using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
namespace EXOOCR.API;

public class OCRService: IOCRService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public OCRService(
        HttpClient httpClient,
        IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public HttpClient GetHttpClient()
    {
        _httpClient.BaseAddress = new Uri(_config.GetValue<string>("OCR-API"));
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _config.GetValue<string>("subscription-key"));
        return _httpClient;
    }

    public StringContent GetImage(string url)
    {
        //var image_url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFJS5144kuVJx2s2b_1XoXPIOF0TXNhTcCcFBIgFO5pA&s";
        var data = new Dictionary<string, string> {
            { "url", url }
            };
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

}

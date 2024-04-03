using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

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
        var data = new Dictionary<string, string> {
            { "url", url }
            };
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    public string GetContainerNumber(List<Blocks> blocks)
    {
        string sentence = "";
        string pattern = @"[A-Z]{4}\d{6,7}";

        foreach (var block in blocks)
        {
            foreach (var line in block.Lines)
            {
                sentence += line.Text;
            }
        }

        var matchedValues = Regex.Matches(sentence.Replace(" ", ""), pattern);
        if (matchedValues.Count == 0)
        {
            return "No Matching Characters";
        }

        var containerNumber = matchedValues[0].Value;
        return containerNumber;
    }
}

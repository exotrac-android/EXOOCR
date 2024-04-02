using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EXOOCR.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _config;

    public TestController(
        HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration config)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _config = config;
    }

    [HttpGet]
    public async Task<string> Test_5()
    {
        var parameters = new Dictionary<string, string> { { "URL", "testUrl.com" }, { "param2", "2" } };
        var encodedContent = new FormUrlEncodedContent(parameters);


        var json = JsonConvert.SerializeObject(parameters, Formatting.Indented);
        var httpRequestMessage = new HttpRequestMessage
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        HttpContext context = _httpContextAccessor.HttpContext;
        _httpClient.BaseAddress = new Uri("https://localhost:44342/api/");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage httpResp = await _httpClient.PostAsync("Setting/TestEndPoint", httpRequestMessage.Content);
        string responseData = await httpResp.Content.ReadAsStringAsync();
        return responseData;
    }

    [HttpGet]
    public async Task<string> Test_4()
    {
        HttpContext context = _httpContextAccessor.HttpContext;
        _httpClient.BaseAddress = new Uri("https://localhost:44342/api/");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage httpResp = await _httpClient.GetAsync("Setting/TestGetEndPoint");
        string responseData = await httpResp.Content.ReadAsStringAsync();
        return responseData;
    }

    [HttpGet]
    public async Task<string> Test_3()
    {
        HttpContext context = _httpContextAccessor.HttpContext;
        _httpClient.BaseAddress = new Uri("https://api.codeitnepal.com/");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage httpResp = await _httpClient.GetAsync("");
        string responseData = await httpResp.Content.ReadAsStringAsync();
        return responseData;
    }

    [HttpGet]
    public async Task<string> Test_2()
    {
        string url = "https://localhost:44342/api/Setting/TestEndPoint";
        HttpResponseMessage response = await _httpClient.PostAsync(url, null);
        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }
        else
        {
            return "Error";
        }
    }

    [HttpGet]
    public async Task<string> Test_1()
    {
        string url = "https://api.codeitnepal.com/";
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }
        else
        {
            return "Error";
        }
    }

}

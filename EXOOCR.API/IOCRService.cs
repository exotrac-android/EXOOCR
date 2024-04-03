namespace EXOOCR.API;

public interface IOCRService
{
    HttpClient GetHttpClient();
    StringContent GetImage(string url);
    string GetContainerNumber(List<Blocks> blocks);
}

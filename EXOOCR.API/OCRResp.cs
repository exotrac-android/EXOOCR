
namespace EXOOCR.API;

public class OCRResp
{
    public string ModelVersion { get; set; }
    public CaptionResult CaptionResult { get; set; }
    public Metadata Metadata { get; set; }
    public ReadResult ReadResult { get; set; }
}

public class CaptionResult
{
    public string Text { get; set; }
    public string Confidence { get; set; }
}

public class Metadata
{
    public string Width { get; set; }
    public string Height { get; set; }
}

public class ReadResult
{
    public List<Blocks> Blocks { get; set; }
}

public class Blocks
{
    public List<Lines> Lines { get; set; }
}

public class Lines
{
    public string Text { get; set; }
    //public BoundingPolygon[] BoundingPolygon { get; set; }
    //public Words[] Words { get; set; }
}

//public class BoundingPolygon
//{
//    public string X { get; set; }
//    public string Y { get; set; }
//}

//public class Words
//{
//    public string Text { get; set; }
//    public List<BoundingPolygon> BoundingPolygon { get; set; }
//    public string Confidence { get; set; }
//}

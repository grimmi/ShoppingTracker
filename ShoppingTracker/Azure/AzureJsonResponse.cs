using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class AzureJsonResponse
{
    [JsonProperty("language")]
    public string Language { get; set; }
    [JsonProperty("textAngle")]
    public float TextAngle { get; set; }
    [JsonProperty("orientation")]
    public string Orientation { get; set; }
    [JsonProperty("regions")]
    public Region[] Regions { get; set; }

    public IEnumerable<Word> Words => Regions.SelectMany(region => region.Lines.SelectMany(line => line.Words));
}

public class Region
{
    [JsonProperty("boundingBox")]
    public string BoundingBox { get; set; }
    [JsonProperty("lines")]
    public Line[] Lines { get; set; }
}


[DebuggerDisplay("{LineText}")]
public class Line
{
    [JsonProperty("boundingBox")]
    public string BoundingBox { get; set; }
    [JsonProperty("words")]
    public Word[] Words { get; set; }

    public string LineText => string.Join(" ", Words.Select(w => w.Text));
}

public class Word
{
    [JsonProperty("boundingBox")]
    public string BoundingBox { get; set; }
    [JsonProperty("text")]
    public string Text { get; set; }
}

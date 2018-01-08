namespace ShoppingTracker
{
    public class OcrSpaceResult
    {
        public Parsedresult[] ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public object ErrorMessage { get; set; }
        public object ErrorDetails { get; set; }
        public string ProcessingTimeInMilliseconds { get; set; }
        public string SearchablePDFURL { get; set; }
    }

    public class Parsedresult
    {
        public Textoverlay TextOverlay { get; set; }
        public int FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }

    public class Textoverlay
    {
        public string[] Lines { get; set; }
        public bool HasOverlay { get; set; }
        public string Message { get; set; }
    }
}

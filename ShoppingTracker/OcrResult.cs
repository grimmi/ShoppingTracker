using System.Collections.Generic;
using System.Drawing;

namespace ShoppingTracker
{
    public class OcrResult
    {
        public IEnumerable<string> Texts { get; set; }
        public IEnumerable<OcrArea> Areas { get; set; }
    }

    public class OcrArea
    {
        public Rectangle BoundingBox { get; set; }
        public string Text { get; set; }
    }
}
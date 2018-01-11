using System.Threading.Tasks;
using Shop = ShoppingTracker;
using IronOcr;
using System;
using System.Linq;

namespace ShoppingTracker46
{
    public class IronOcrService : Shop.IOcrService
    {
        public Task<Shop.OcrResult> Process(byte[] image)
        {
            throw new System.NotImplementedException();
        }

        public Task<Shop.OcrResult> Process(string file)
        {
            var ocr = new AutoOcr();
            var result = ocr.Read(file);

            var ocrResult = new Shop.OcrResult()
            {
                Areas = Enumerable.Empty<Shop.OcrArea>(),
                Texts = result.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries )
            };

            return Task.FromResult(ocrResult);
        }
    }
}

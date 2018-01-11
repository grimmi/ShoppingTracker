using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShoppingTracker
{
    public class AzureOcrService : IOcrService
    {
        private string _apiKey;
        private string _uriBase;
        private HttpClient _client;

        public AzureOcrService(string apiKey, string uriBase, HttpClient client)
        {
            _apiKey = apiKey;
            _uriBase = uriBase;
            _client = client;
        }

        public async Task<OcrResult> Process(byte[] image)
        {
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

            var uri = $"{_uriBase}?language=unk&detectOrientation=true";

            using (var content = new ByteArrayContent(image))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var response = await _client.PostAsync(uri, content);

                var ocrResponse = JsonConvert.DeserializeObject<AzureJsonResponse>(await response.Content.ReadAsStringAsync());

                var words = new List<OcrArea>();
                foreach (var word in ocrResponse.Words)
                {
                    var boundingBox = word.BoundingBox.Split(',').Select(v => int.Parse(v)).ToArray();
                    var rectangle = new Rectangle(boundingBox[0], boundingBox[1], boundingBox[2] - boundingBox[0], boundingBox[3] - boundingBox[1]);
                    words.Add(new OcrArea { BoundingBox = rectangle, Text = word.Text });
                }
                return new OcrResult { Areas = words, Texts = words.Select(w => w.Text).ToList() };
            }
        }

        public Task<OcrResult> Process(string file)
        {
            throw new System.NotImplementedException();
        }
    }
}

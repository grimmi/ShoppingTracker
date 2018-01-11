using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingTracker
{
    class ObservableStringContent : StringContent
    {
        public ObservableStringContent(string content) : base(content)
        {
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return Task.Run(() =>
            {
                using (var content = base.ReadAsStreamAsync().Result)
                {
                    var buffer = new byte[512];
                    int length;

                    while ((length = content.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stream.Write(buffer, 0, length);
                        stream.Flush();
                    }
                }
            });
        }
    }

    public class OcrSpaceService : IOcrService
    {
        private static string apiUrl = "https://api.ocr.space/parse/image";
        private string _apiKey;

        public OcrSpaceService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<OcrResult> Process(byte[] image)
        {
            using (var content = new MultipartFormDataContent())
            {
                var base64Image = Convert.ToBase64String(image);
                content.Add(new StringContent(_apiKey), "apikey");
                content.Add(new ObservableStringContent("data:image/jpeg;base64," + base64Image), "base64Image");

                using (var client = new HttpClient())
                using (var response = await client.PostAsync(apiUrl, content))
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    var ocrResponse = JsonConvert.DeserializeObject<OcrSpaceResult>(responseText);
                    return new OcrResult
                    {
                        Areas = Enumerable.Empty<OcrArea>(),
                        Texts = ocrResponse.ParsedResults.First().ParsedText.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    };
                }
            }
        }

        public Task<OcrResult> Process(string file)
        {
            throw new NotImplementedException();
        }
    }
}

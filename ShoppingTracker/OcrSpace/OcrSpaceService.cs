using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingTracker
{
    public class OcrSpaceService : IOcrService
    {
        public OcrSpaceService(string apiKey)
        {

        }

        public Task<OcrResult> Process(byte[] image)
        {
            throw new NotImplementedException();
        }
    }
}

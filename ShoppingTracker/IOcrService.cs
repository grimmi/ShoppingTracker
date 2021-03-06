﻿using System.Threading.Tasks;

namespace ShoppingTracker
{
    public interface IOcrService
    {
        Task<OcrResult> Process(byte[] image);
        Task<OcrResult> Process(string file);
    }
}

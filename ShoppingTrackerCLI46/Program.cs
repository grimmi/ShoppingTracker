using ShoppingTracker46;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingTrackerCLI46
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new IronOcrService();
            foreach (var file in Directory.EnumerateFiles(@"c:\temp\tesseract"))
            {
                var result = service.Process(file).Result;
                Console.WriteLine("*******************************" + file);

                foreach (var text in result.Texts)
                {
                    Console.WriteLine(text);
                }
            }
            Console.WriteLine("all done");
            Console.ReadKey();
        }
    }
}

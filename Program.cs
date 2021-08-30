using System;
using IronPdf;

namespace IronPDFPublishBug
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"IronPDF is licensed: {License.IsLicensed}");
            var renderer = new ChromePdfRenderer();
            var result = renderer.RenderHtmlAsPdf("<h1>Hello multiple entries</h1>");
            result.SaveAs("hello.pdf");
        }
    }
}

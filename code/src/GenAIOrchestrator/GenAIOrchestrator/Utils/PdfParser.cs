using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.IO;
using System.Threading.Tasks;

public class PdfParser
{
    public static async Task<string> ExtractTextFromPdf(string filePath)
    {
        using (PdfReader reader = new PdfReader(filePath))
        using (PdfDocument pdfDoc = new PdfDocument(reader))
        {
            string extractedText = "";
            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                extractedText += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));
            }
            return await Task.FromResult(extractedText);
        }
    }
}

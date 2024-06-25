using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using PdfGenerator.Application.Interfaces;

namespace PdfGenerator.Application.Templates
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        private readonly ITemplateService _templateService;

        public PdfGeneratorService(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        public async Task<string> GeneratePdfFromTemplateAsync(Guid templateId, object context)
        {
            var compiledHtml = await _templateService.CompileTemplateWithModel(templateId, context);
            var pdfBytes = GeneratePdf(compiledHtml);
            return ConvertToBase64(pdfBytes);
        }

        private byte[] GeneratePdf(string htmlContent)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var document = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    using (var stringReader = new StringReader(htmlContent))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);
                    }
                    document.Close();
                }
                return memoryStream.ToArray();
            }
        }

        private string ConvertToBase64(byte[] pdfBytes)
        {
            return Convert.ToBase64String(pdfBytes);
        }
    }
}

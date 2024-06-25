namespace PdfGenerator.Application.Interfaces
{
    public interface IPdfGeneratorService
    {
        Task<string> GeneratePdfFromTemplateAsync(Guid templateId, object context);
    }
}

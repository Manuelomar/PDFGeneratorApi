namespace PdfGenerator.Application.TemplateField.Request
{
    public class PdfRequestDto
    {
        public Guid TemplateId { get; set; }
        public dynamic Context { get; set; }
    }
}

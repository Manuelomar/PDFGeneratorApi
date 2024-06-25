using PdfGenerator.Application.TemplateField.Responses;

namespace PdfGenerator.Application.Templates.Responses
{
    public class TemplateResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TemplateFieldResponseDto> Fields { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public TemplateResponseDto()    
        {
            Fields = new List<TemplateFieldResponseDto>();
        }

    }
}

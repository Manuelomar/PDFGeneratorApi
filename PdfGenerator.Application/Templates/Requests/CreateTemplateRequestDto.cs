using PdfGenerator.Application.TemplateField.Responses;

namespace PdfGenerator.Application.Template.Requests
{
    public class CreateTemplateRequestDto
    {
        public string Name { get; set; }
        public List<TemplateFieldResponseDto> Fields { get; set; }

        public CreateTemplateRequestDto()
        {
            Fields = new List<TemplateFieldResponseDto>();
        }
    }
}

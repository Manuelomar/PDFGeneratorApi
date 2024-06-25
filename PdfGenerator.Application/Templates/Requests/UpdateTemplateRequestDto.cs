using PdfGenerator.Application.TemplateField.Responses;
using System.Text.Json.Serialization;

namespace PdfGenerator.Application.Template.Requests
{
    public class UpdateTemplateRequestDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TemplateFieldResponseDto> Fields { get; set; }

        public UpdateTemplateRequestDto()
        {
            Fields = new List<TemplateFieldResponseDto>();
        }
    }
}

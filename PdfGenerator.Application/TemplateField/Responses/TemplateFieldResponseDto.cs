using System.Text.Json.Serialization;

namespace PdfGenerator.Application.TemplateField.Responses
{
    public class TemplateFieldResponseDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string DestinationName { get; set; }
        public string Skills { get; set; }
        public string RelevantAreas { get; set; }
        public string From { get; set; }
        public string DesiredPosition { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
}

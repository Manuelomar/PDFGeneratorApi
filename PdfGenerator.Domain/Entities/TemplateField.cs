namespace PdfGenerator.Domain.Entities
{
    public class TemplateField : BaseEntity
    {
        public Guid TemplateId { get; set; }
        public Template Template { get; set; } 
        public string DestinationName { get; set; }
        public string Skills { get; set; }
        public string RelevantAreas { get; set; }
        public string From { get; set; }
        public string DesiredPosition { get; set; }

    }
}

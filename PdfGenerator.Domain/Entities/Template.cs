namespace PdfGenerator.Domain.Entities
{
    public class Template : BaseEntity
    {
        public string Name { get; set; }
        public List<TemplateField> Fields { get; set; }

        public Template()
        {
            Fields = new List<TemplateField>();
        }
    }
}

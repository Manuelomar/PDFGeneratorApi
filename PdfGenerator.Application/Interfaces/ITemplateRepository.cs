using TemplatesEntity = PdfGenerator.Domain.Entities.Template;

namespace PdfGenerator.Application.Interfaces
{
    public interface ITemplateRepository : IBaseRepository<TemplatesEntity>
    {
        Task<TemplatesEntity> GetByName(string name);
    }
}


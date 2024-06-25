using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.Interfaces;
using PdfGenerator.Infrastructure.Context;
using TemplatesEntity = PdfGenerator.Domain.Entities.Template;
namespace PdfGenerator.Infrastructure.Repository
{
    public class TemplateRepository : BaseRepository<TemplatesEntity>, ITemplateRepository
    {
        public TemplateRepository(IDbContext context) : base(context)
        {
        }

        public async Task<TemplatesEntity> GetByName(string name)
        {
            return await _db.FirstOrDefaultAsync(template => template.Name == name);
        }
    }
}

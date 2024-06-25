using PdfGenerator.Application.Common;
using PdfGenerator.Application.Template.Requests;
using PdfGenerator.Application.Templates.Responses;

namespace PdfGenerator.Application.Interfaces
{
    public interface ITemplateService
    {
        Task<TemplateResponseDto> CreateTemplate(CreateTemplateRequestDto request, CancellationToken cancellationToken);
        Task<TemplateResponseDto> UpdateTemplateAsync(UpdateTemplateRequestDto request, CancellationToken cancellationToken);
        Task<TemplateResponseDto> DeleteTemplateAsync(Guid id);
        Task<TemplateResponseDto> GetTemplateById(Guid id);
        Task<string> CompileTemplateWithModel(Guid templateId, object model);
        Task<Paged<TemplateResponseDto>> GetPagedTemplate(PaginationQuery paginationQuery, CancellationToken cancellationToken);

    }
}

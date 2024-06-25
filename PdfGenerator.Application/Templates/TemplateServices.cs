using AutoMapper;
using AutoMapper.QueryableExtensions;
using HandlebarsDotNet;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.Common;
using PdfGenerator.Application.Interfaces;
using PdfGenerator.Application.Template.Requests;
using PdfGenerator.Application.Templates.Responses;
using TemplatesEntity = PdfGenerator.Domain.Entities.Template;

namespace PdfGenerator.Application.Templates
{
    public class TemplateServices : ITemplateService
    {
        private readonly IBaseRepository<TemplatesEntity> _templateRepository;
        private readonly IMapper _mapper;

        public TemplateServices(IBaseRepository<TemplatesEntity> templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            _mapper = mapper;
        }

        public Task<Paged<TemplateResponseDto>> GetPagedTemplate(PaginationQuery paginationQuery, CancellationToken cancellationToken)
        {
            var query = _templateRepository.Query().OrderByDescending(c => c.CreatedDate);

            var queryMapped = query
             .ProjectTo<TemplateResponseDto>(_mapper.ConfigurationProvider);

            var paginatedResult = queryMapped
            .Paginate(paginationQuery.PageNumber, paginationQuery.PageSize, cancellationToken);

            return paginatedResult;
        }
        public async Task<TemplateResponseDto> DeleteTemplateAsync(Guid id)
        {
            var deletedTemplate = await _templateRepository.Delete(id);
            return _mapper.Map<TemplateResponseDto>(deletedTemplate);
        }

        public async Task<TemplateResponseDto> GetTemplateById(Guid id)
        {
            var template = await _templateRepository
                .Query().Include(t => t.Fields)
                .FirstOrDefaultAsync(t => t.Id == id);
            return _mapper.Map<TemplateResponseDto>(template);
        }

        public async Task<TemplateResponseDto> UpdateTemplateAsync(UpdateTemplateRequestDto request, CancellationToken cancellationToken)
        {
            var existingTemplate = await _templateRepository.GetById(request.Id);
            if (existingTemplate == null)
            {
                throw new KeyNotFoundException("Template not found.");
            }

            _mapper.Map(request, existingTemplate);
            var updatedTemplate = await _templateRepository.UpdateAsync(existingTemplate);
            return _mapper.Map<TemplateResponseDto>(updatedTemplate);
        }

        private string CompileTemplate(string htmlTemplate, object model)
        {
            var handlebarsTemplate = Handlebars.Compile(htmlTemplate);
            return handlebarsTemplate(model);
        }

        public async Task<TemplateResponseDto> CreateTemplate(CreateTemplateRequestDto request, CancellationToken cancellationToken)
        {
            var template = _mapper.Map<TemplatesEntity>(request);
            var result = await _templateRepository.AddAsync(template, cancellationToken);
            var dto = _mapper.Map<TemplateResponseDto>(result);
            return dto;
        }
        public async Task<string> CompileTemplateWithModel(Guid templateId, object model)
        {
            var template = await GetTemplateById(templateId);
            if (template == null)
                throw new KeyNotFoundException("Template not found.");

            string htmlTemplate = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>{template.Name}</title>
                </head>
                <body>
                    <h1>Carta de Solicitud</h1>
                    <p>Estimado/a {template.Fields.FirstOrDefault().DestinationName},</p>
                    <p>
                        Me dirijo a usted para expresar mi interés en el puesto de {template.Fields.FirstOrDefault().DesiredPosition}
                        en su empresa. Soy un/a profesional con experiencia en el campo y estoy
                        entusiasmado/a con la oportunidad de formar parte de su equipo.
                    </p>
                    <h2>Habilidades:</h2>

                    <p>Adjunto a esta carta encontrará mi currículum vitae, que detalla mi formación
                    académica y mi experiencia laboral relevante. Estoy seguro/a de que mis
                    habilidades y conocimientos en {template.Fields.FirstOrDefault().RelevantAreas} serán un valioso aporte para su
                    organización.</p>
                    <p>Quedo a su disposición para proporcionar cualquier información adicional que
                    pueda necesitar. Agradezco de antemano su consideración y espero tener la
                    oportunidad de discutir cómo puedo contribuir al éxito de su empresa.</p>
                                <p>Atentamente</p>
                    <p>{template.Fields.FirstOrDefault().DestinationName}</p>
                    <p>{template.Fields.FirstOrDefault().From}</p>
                    <p>{template.Fields.FirstOrDefault().CreatedDate}</p>
                </body>
                </html>";

            var compiledContent = CompileTemplate(htmlTemplate, model);
            return compiledContent;
        }




    }
}

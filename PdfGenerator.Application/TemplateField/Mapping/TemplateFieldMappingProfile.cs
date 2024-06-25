using AutoMapper;
using PdfGenerator.Application.TemplateField.Responses;
using FieldEntity = PdfGenerator.Domain.Entities.TemplateField;
namespace PdfGenerator.Application.TemplateField.Mapping
{
    public class TemplateFieldMappingProfile : Profile
    {
        public TemplateFieldMappingProfile()
        {
            CreateMap<FieldEntity, TemplateFieldResponseDto>();
            CreateMap<TemplateFieldResponseDto, FieldEntity>();
        }
    }
}

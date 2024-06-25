using PdfGenerator.Application.Templates.Responses;
using AutoMapper;
using PdfGenerator.Application.Template.Requests;
using TemplateEntity = PdfGenerator.Domain.Entities.Template;

namespace PdfGenerator.Application.Templates.Mapping
{
    public class TemplateMappingProfile : Profile
    {
        public TemplateMappingProfile()
        {
            CreateMap<TemplateEntity, TemplateResponseDto>();
            CreateMap<UpdateTemplateRequestDto, TemplateEntity>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields));

            CreateMap<CreateTemplateRequestDto, TemplateEntity>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields));

            CreateMap<TemplateEntity, UpdateTemplateRequestDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields));
        }
    }

}

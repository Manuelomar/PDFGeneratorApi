using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PdfGenerator.Application.Interfaces;
using PdfGenerator.Application.Templates;
using System.Reflection;

namespace PdfGenerator.Application
{
    public static class IoC
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddTransient<ITemplateService, TemplateServices>();

            services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
            return services;
        }
    }
}

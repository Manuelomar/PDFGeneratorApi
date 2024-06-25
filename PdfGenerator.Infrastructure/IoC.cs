using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfGenerator.Application.Interfaces;
using PdfGenerator.Infrastructure.Context;
using PdfGenerator.Infrastructure.Repository;

namespace PdfGenerator.Infrastructure
{
    public static class IoC
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbContext, ApplicationDbContext>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ITemplateRepository, TemplateRepository>(); 

            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            return services;

        }
    }
}

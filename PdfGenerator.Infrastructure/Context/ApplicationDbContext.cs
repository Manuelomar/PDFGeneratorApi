using Microsoft.EntityFrameworkCore;
using PdfGenerator.Domain.Entities;

namespace PdfGenerator.Infrastructure.Context
{
    public interface IApplicationDbContext : IDbContext { }

    public class ApplicationDbContext : BaseDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateField> TemplateFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Template>().HasKey(t => t.Id);
            modelBuilder.Entity<TemplateField>().HasKey(tf => tf.Id);

            modelBuilder.Entity<TemplateField>()
                .HasOne(tf => tf.Template)
                .WithMany(t => t.Fields)
                .HasForeignKey(tf => tf.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

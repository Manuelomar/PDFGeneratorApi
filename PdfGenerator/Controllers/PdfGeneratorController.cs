using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Application.Interfaces;

namespace PdfGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfGeneratorController : ControllerBase
    {
        private readonly IPdfGeneratorService _pdfGeneratorService;

        public PdfGeneratorController(IPdfGeneratorService pdfGeneratorService)
        {
            _pdfGeneratorService = pdfGeneratorService;
        }

        [HttpPost("generate-pdf/{templateId}")]
        public async Task<IActionResult> GeneratePdf(Guid templateId, [FromBody] dynamic context)
        {
            try
            {
                var pdfBase64 = await _pdfGeneratorService.GeneratePdfFromTemplateAsync(templateId, context);
                if (string.IsNullOrEmpty(pdfBase64))
                {
                    return NotFound("No se pudo generar el PDF, verifique la plantilla y los datos de contexto.");
                }
                return Ok(new { Pdf = pdfBase64 });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

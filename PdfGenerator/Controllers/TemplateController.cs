using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Application.Common;
using PdfGenerator.Application.Interfaces;
using PdfGenerator.Application.Template.Requests;
using System;
using System.Threading.Tasks;

namespace PdfGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedTemplate([FromQuery] PaginationQuery paginationQuery, CancellationToken cancellationToken = default)
        {
            var result = await _templateService.GetPagedTemplate(paginationQuery, cancellationToken);
            return Ok(BaseResponse.Ok(result));
        }


        [HttpPost]
        public async Task<IActionResult> CreateTemplate(CreateTemplateRequestDto templateDto)
        {
            var result = await _templateService.CreateTemplate(templateDto, CancellationToken.None);
            return CreatedAtAction(nameof(GetTemplateById), new { id = result.Id }, result);
        }

        //[HttpPost("{id}/compile")]
        //public async Task<IActionResult> CompileTemplate(Guid id, [FromBody] dynamic model)
        //{
        //    try
        //    {
        //        var compiledContent = await _templateService.CompileTemplateWithModel(id, model);
        //        return Ok(compiledContent);
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplateById(Guid id)
        {
            var result = await _templateService.GetTemplateById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplate(Guid id, UpdateTemplateRequestDto templateDto)
        {
            templateDto.Id = id;
            var result = await _templateService.UpdateTemplateAsync(templateDto, CancellationToken.None);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate(Guid id)
        {
            await _templateService.DeleteTemplateAsync(id);
            return NoContent();
        }


    }
}

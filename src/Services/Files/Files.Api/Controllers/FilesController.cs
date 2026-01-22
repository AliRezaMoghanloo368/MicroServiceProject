using AutoMapper;
using Files.Application.Dtos;
using Files.Application.Interfaces;
using Files.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Files.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        #region Constructor
        private readonly IMapper _mapper;
        private readonly IFilesRepository _repo;
        public FilesController(IFilesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        #endregion

        #region Get File
        [HttpGet("{id:guid}", Name = "GetFile")]
        [ProducesResponseType(typeof(FilesDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FilesDto), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<FilesDto?>> GetByIdAsync(Guid id)
        {
            var file = await _repo.GetByIdAsync(id);

            if (file == null)
                return NotFound();

            var result = _mapper.Map<FilesDto>(file);
            return Ok(result);
        }
        #endregion

        #region Get Files
        [HttpGet("{entityName}/{entityId}", Name = "GetFiles")]
        [ProducesResponseType(typeof(IReadOnlyList<FilesDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<FilesDto>>> GetFilesAsync(string entityName, string entityId)
        {
            var files = await _repo.GetFilesAsync(entityName, entityId);
            var result = _mapper.Map<IReadOnlyList<FilesDto>>(files);
            return Ok(result);
        }
        #endregion

        #region Create Files
        [HttpPost(Name = "CreateFiles")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateFiles([FromForm] CreateFileDto dto)
        {
            using var ms = new MemoryStream();
            await dto.FileContent.CopyToAsync(ms);

            var file = new FilesEntity
            {
                Id = Guid.NewGuid(),
                EntityName = dto.EntityName,
                EntityId = dto.EntityId,
                FileContent = ms.ToArray(),
                UploadAt = DateTime.UtcNow
            };

            var result = await _repo.CreateAsync(file);
            return Ok(result);
        }

        #endregion

        #region Update Files
        [HttpPut(Name = "UpdateFiles")]
        public async Task<IActionResult> UpdateFiles([FromForm] UpdateFileDto dto)
        {
            var fileEntity = _mapper.Map<FilesEntity>(dto);

            if (dto.FileContent != null)
            {
                using var ms = new MemoryStream();
                await dto.FileContent.CopyToAsync(ms);
                fileEntity.FileContent = ms.ToArray();
            }

            var result = await _repo.UpdateAsync(fileEntity);
            return Ok(result);
        }
        #endregion

        #region Delete Files
        [HttpDelete("{id}", Name = "DeleteFiles")]
        public async Task<ActionResult> DeleteFiles(Guid id)
        {
            var result = await _repo.DeleteAsync(id);
            return Ok(result);
        }
        #endregion
    }
}

using AutoMapper;
using Files.Application.Dtos;
using Files.Application.Interfaces;
using Files.Domain.Models;
using Main.Application.Features.Filess.Commands.CreateFiles;
using Main.Application.Features.Filess.Commands.DeleteFiles;
using Main.Application.Features.Filess.Commands.UpdateFiles;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Files.Api.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{id}", Name = "GetFile")]
        [ProducesResponseType(typeof(FilesDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FilesDto?>> GetByIdAsync(string id)
        {
            var file = await _repo.GetByIdAsync(id);
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
        public async Task<ActionResult<FilesEntity>> CreateFiles([FromBody] CreateFileDto dto)
        {
            var file = _mapper.Map<FilesEntity>(dto);
            var result = await _repo.CreateAsync(file);

            //// For log
            //await _service.CreateHistoryAsync("files", result.Data.Id.ToString(), HistoryAction.add);

            return Ok(result);
        }
        #endregion

        #region Update Files
        [HttpPut(Name = "UpdateFiles")]
        public async Task<ActionResult> UpdateFiles([FromBody] UpdateFileDto dto)
        {
            var file = _mapper.Map<FilesEntity>(dto);
            var result = await _repo.UpdateAsync(file);
            return Ok(result);
        }
        #endregion

        #region Delete Files
        [HttpDelete("{id}", Name = "DeleteFiles")]
        public async Task<ActionResult> DeleteFiles(string id)
        {
            var result = await _repo.DeleteAsync(id);
            return Ok(result);
        }
        #endregion
    }
}

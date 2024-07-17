using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.Interfaces.Base;
using OnlineStore.Infrastructure.Filters;
using OnlineStore.Infrastructure.Pagination;
using OnlineStore.Models.Base;

namespace OnlineStore.Api.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiResultFilter]
    public abstract class CrudBaseController<TEntity, TRequestDto, TResponseDto> : ControllerBase
         where TEntity : EntityBase
         where TRequestDto : class
         where TResponseDto : class
    {
        private readonly IBaseService<TEntity, TRequestDto, TResponseDto> _service;
        public CrudBaseController(IBaseService<TEntity, TRequestDto, TResponseDto> service)
        {
            _service = service;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchDto searchDto)
        {
            var result = await _service.SearchAsync(searchDto);
            return Ok(result);
        }

        [HttpPost("get-all")]
        public async Task<ActionResult<IEnumerable<TResponseDto>>> GetAll([FromBody] PaginationRequestDto pagination)
        {
            var entities = await _service.GetAllAsync(pagination);
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TResponseDto>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TResponseDto>> Add(TRequestDto requestDto)
        {
            var responseDto = await _service.AddAsync(requestDto);
            return CreatedAtAction(nameof(GetById), responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TRequestDto requestDto)
        {
            await _service.UpdateAsync(id, requestDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}

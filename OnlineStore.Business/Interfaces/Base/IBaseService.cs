using OnlineStore.Infrastructure.Pagination;
using OnlineStore.Models.Base;
using System.Linq.Expressions;

namespace OnlineStore.Business.Interfaces.Base
{
    public interface IBaseService<TEntity, TRequestDto, TResponseDto>
          where TEntity : EntityBase
          where TRequestDto : class
          where TResponseDto : class
    {
        Task<TResponseDto> AddAsync(TRequestDto requestDto);
        Task UpdateAsync(int id, TRequestDto requestDto);
        Task DeleteAsync(int id);
        Task<TResponseDto> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TResponseDto>> GetAllAsync(PaginationRequestDto pagination, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TResponseDto>> SearchAsync(SearchDto searchDto);
    }
}

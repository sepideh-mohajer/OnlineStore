using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using OnlineStore.Business.Interfaces.Base;
using OnlineStore.DataAccess.Interfaces;
using OnlineStore.Infrastructure.Helpers;
using OnlineStore.Infrastructure.Pagination;
using OnlineStore.Models.Base;
using System.Linq.Expressions;

namespace OnlineStore.Business.Services.Base
{
    public class BaseService<TEntity, TRequestDto, TResponseDto> : IBaseService<TEntity, TRequestDto, TResponseDto>
        where TEntity : EntityBase
        where TRequestDto : class
        where TResponseDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TResponseDto> AddAsync(TRequestDto requestDto)
        {
            var entity = _mapper.Map<TEntity>(requestDto);
            entity.CreatedOn = DateTime.Now;
            entity.IsActive = true;
            var createdEntity = await _unitOfWork.Repository<TEntity>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TResponseDto>(createdEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<TEntity>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TResponseDto>> GetAllAsync(PaginationRequestDto pagination, params Expression<Func<TEntity, object>>[] includes)
        {
            var entities = _unitOfWork.Repository<TEntity>().GetAllAsync(includes);
            var mappedEntity = _mapper.ProjectTo<TResponseDto>(entities);
            var pagedEntity = await PagedList<TResponseDto>.ToPagedListAsync(mappedEntity, pagination.PageNumber, pagination.PageSize);
            return pagedEntity;
        }

        public async Task<TResponseDto> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id, includes);
            return _mapper.Map<TResponseDto>(entity);
        }

        public async Task UpdateAsync(int id, TRequestDto requestDto)
        {
            var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
            if (entity == null) throw new ArgumentException("Entity not found");

            entity.ModifiedOn = DateTime.Now;
            _mapper.Map(requestDto, entity);
            await _unitOfWork.Repository<TEntity>().UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TResponseDto>> SearchAsync(SearchDto searchDto)
        {
            var query = _unitOfWork.Repository<TEntity>().GetAllAsync();

            // Apply search filter
            if (searchDto.SearchCriteria != null && searchDto.SearchCriteria.Any())
            {
                var searchExpression = ExpressionHelper.BuildSearchExpression<TEntity>(searchDto.SearchCriteria);
                query = query.Where(searchExpression);
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(searchDto.SortBy))
            {
                var parameter = Expression.Parameter(typeof(TEntity), "e");
                var property = Expression.Property(parameter, searchDto.SortBy);
                var lambda = Expression.Lambda(property, parameter);
                var methodName = searchDto.SortDescending ? "OrderByDescending" : "OrderBy";
                var resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), property.Type }, query.Expression, lambda);
                query = query.Provider.CreateQuery<TEntity>(resultExpression);
            }

            // Apply pagination
            var mappedEntity = _mapper.ProjectTo<TResponseDto>(query);
            var pagedEntity = await PagedList<TResponseDto>.ToPagedListAsync(mappedEntity, searchDto.Pagination.PageNumber, searchDto.Pagination.PageSize);

            return (pagedEntity);
        }
    }
}

using AutoMapper;
using CMS.Repositories;

namespace CMS.Services
{
    public abstract class BaseService<TEntity, TResponse, TCreateRequest> : IBaseService<TResponse, TCreateRequest>
        where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        protected BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TResponse>>(entities);
        }

        public virtual async Task<TResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TResponse>(entity);
        }

        public virtual async Task<TResponse> CreateAsync(TCreateRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);
            var createdEntity = await _repository.CreateAsync(entity);
            return _mapper.Map<TResponse>(createdEntity);
        }

        public virtual async Task<TResponse> UpdateAsync(int id, TCreateRequest request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _mapper.Map(request, entity);
                await _repository.UpdateAsync(entity);
                return _mapper.Map<TResponse>(entity);
            }
            return default;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                await _repository.DeleteAsync(id);
                return true;
            }
            return false;
        }
    }
}

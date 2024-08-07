using AutoMapper;
using Domain.Interface.Repository.Common;
using Domain.Interface.Service.Common;

namespace Service.Common
{
    public abstract class BaseService<TEntity, TRequest, TResponse> : IBaseService<TRequest, TResponse>
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _uow;

        protected BaseService(IBaseRepository<TEntity> repository, IMapper mapper, IUnitOfWork uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
        }

        public virtual async Task<IEnumerable<TResponse>> Get()
        {
            var entities = await _repository.Get();
            return _mapper.Map<IEnumerable<TResponse>>(entities);
        }

        public virtual async Task<TResponse> Get(Guid uuid)
        {
            var entity = await _repository.Get(uuid);
            return _mapper.Map<TResponse>(entity);
        }

        public virtual async Task<TResponse> Create(TRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);
            await _repository.Create(entity);
            _uow.Commit();
            return _mapper.Map<TResponse>(entity);
        }

        public virtual async Task<TResponse> Update(Guid uuid, TRequest request)
        {
            var entity = await _repository.Get(uuid);
            _mapper.Map(request, entity);
            await _repository.Update(entity);
            _uow.Commit();
            return _mapper.Map<TResponse>(entity);
        }

        public virtual async Task Delete(Guid uuid)
        {
            await _repository.Delete(uuid);
            _uow.Commit();
        }
    }
}

namespace Domain.Interface.Service.Common
{
    public interface IBaseService<TRequest, TResponse>
    {
        Task<IEnumerable<TResponse>> Get();
        Task<TResponse> Get(Guid uuid);
        Task<TResponse> Create(TRequest request);
        Task<TResponse> Update(Guid uuid, TRequest request);
        Task Delete(Guid uuid);
    }
}

namespace Domain.Interface.Repository.Common
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(Guid uuid);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(Guid uuid);
    }
}

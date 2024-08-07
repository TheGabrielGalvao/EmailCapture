namespace Domain.Interface.Repository.Common
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}

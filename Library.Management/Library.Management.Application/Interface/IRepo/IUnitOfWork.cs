namespace Library.Management.Application.Interface.Common
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}

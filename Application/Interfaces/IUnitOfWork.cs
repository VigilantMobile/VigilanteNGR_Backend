using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Interfaces.Repositories.Location;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositoryAsync<T> Repository<T>() where T : class;
        ISecurityTipRepositoryAsync SecurityTips { get; }
        ISecurityTipCategoryRepositoryAsync SecurityTipCategories { get; }
        IStateRepositoryAsync States { get; }
        ICircleRepositoryAsync Circles { get; }
        ILGARepositoryAsync LGAs { get; }
        ITownRepositoryAsync Towns { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

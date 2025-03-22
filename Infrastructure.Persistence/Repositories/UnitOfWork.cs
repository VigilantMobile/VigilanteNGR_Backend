using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Interfaces.Repositories.Location;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories.SecurityTips;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed;
        private IDbContextTransaction _transaction;
        private readonly Dictionary<Type, object> _repositories;

        // Repository instances
        private ISecurityTipRepositoryAsync _securityTips;
        private ISecurityTipCategoryRepositoryAsync _securityTipCategories;
        private IStateRepositoryAsync _states;
        private ILGARepositoryAsync _lgas;
        private ITownRepositoryAsync _towns;
        private ICircleRepositoryAsync _circles;
        public UnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _repositories = new Dictionary<Type, object>();
        }

        public IGenericRepositoryAsync<T> Repository<T>() where T : class
        {
            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepositoryAsync<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(type), _context);
                _repositories[type] = repositoryInstance;
            }

            return (IGenericRepositoryAsync<T>)_repositories[type];
        }

        public ISecurityTipRepositoryAsync SecurityTips =>
            _securityTips ??= _serviceProvider.GetService<ISecurityTipRepositoryAsync>();

        public ISecurityTipCategoryRepositoryAsync SecurityTipCategories =>
            _securityTipCategories ??= _serviceProvider.GetService<ISecurityTipCategoryRepositoryAsync>();

        public IStateRepositoryAsync States =>
            _states ??= _serviceProvider.GetService<IStateRepositoryAsync>();

        public ILGARepositoryAsync LGAs =>
            _lgas ??= _serviceProvider.GetService<ILGARepositoryAsync>();

        public ITownRepositoryAsync Towns =>
            _towns ??= _serviceProvider.GetService<ITownRepositoryAsync>();

        public ICircleRepositoryAsync Circles =>
           _circles ??= _serviceProvider.GetService<ICircleRepositoryAsync>();
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}

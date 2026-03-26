using PacmanGame.DataAccess.Repositories;
using PacmanGame.DataAccess.Repositories.Interfaces;
using System;
using System.Data.Entity;

namespace PacmanGame.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _disposed;
        private DbContextTransaction _transaction;

        private IUserRepository _userRepository;
        private IGameStateRepository _gameStateRepository;

        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUserRepository Users =>
            _userRepository ?? (_userRepository = new UserRepository(_context));

        public IGameStateRepository GameStates =>
            _gameStateRepository ?? (_gameStateRepository = new GameStateRepository(_context));

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сохранения: {ex.Message}");
                throw;
            }
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _transaction?.Commit();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка коммита транзакции: {ex.Message}");
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _transaction?.Rollback();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка отката транзакции: {ex.Message}");
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void ResetChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
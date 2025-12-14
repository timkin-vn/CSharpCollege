using PacmanGame.DataAccess.Repositories.Interfaces;
using System;

namespace PacmanGame.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IGameStateRepository GameStates { get; }

        int SaveChanges();

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void ResetChanges();
    }
}
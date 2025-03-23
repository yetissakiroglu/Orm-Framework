using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Code.OrmFramework.Transactions
{
    public class TransactionService : ITransaction
    {
        private readonly DbContext _context;
        private readonly IDbContextTransaction _transaction;

        public TransactionService(DbContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync() => await _transaction.CommitAsync();

        public async Task RollbackAsync() => await _transaction.RollbackAsync();

        public void Dispose() => _transaction.Dispose();
    }
}

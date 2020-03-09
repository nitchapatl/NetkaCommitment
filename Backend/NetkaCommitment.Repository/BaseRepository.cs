using Microsoft.EntityFrameworkCore.Storage;
using NetkaCommitment.Data.EFModels;
using System;

namespace NetkaCommitment.Repository
{
    public class BaseRepository : IDisposable
    {
        protected NetkaCommitmentContext db = null;
        protected IDbContextTransaction oTransaction = null;
        public BaseRepository() {
            db = new NetkaCommitmentContext();
        }

        public BaseRepository(bool usingTransaction)
        {
            db = new NetkaCommitmentContext();
            if (usingTransaction)
            {
                oTransaction = db.Database.BeginTransaction();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (oTransaction != null)
                {
                    oTransaction.Dispose();
                    db.Dispose();
                }
            }
        }
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using NetkaCommitment.Data.EFModels;
using NetkaCommitment.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetkaCommitment.Business
{
    public class BaseBiz
    {
        protected NetkaCommitmentContext db = null;
        protected IDbContextTransaction oTransaction = null;
        protected readonly AccessTokenRepository oAccessTokenRepository = null;
        protected readonly AccessLogRepository oAccessLogRepository = null;
        
        public BaseBiz()
        {
            db = new NetkaCommitmentContext();
            oAccessTokenRepository = new AccessTokenRepository();
            oAccessLogRepository = new AccessLogRepository();
        }
        public BaseBiz(bool usingTransaction)
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

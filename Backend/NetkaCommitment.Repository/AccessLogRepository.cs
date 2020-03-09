using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetkaCommitment.Repository
{
    public class AccessLogRepository : BaseRepository, IRepository<TAccessLog>
    {
        public bool Delete(TAccessLog obj)
        {
            TAccessLog oResult = db.TAccessLog.Where(t => t.AccessLogId == obj.AccessLogId).FirstOrDefault();
            if (oResult == null)
            {
                return false;
            }

            db.Entry(obj).State = EntityState.Deleted;
            db.SaveChanges();

            return true;
        }

        public IQueryable<TAccessLog> Get()
        {
            return db.TAccessLog.AsQueryable<TAccessLog>();
        }

        public TAccessLog Get(TAccessLog obj)
        {
            return db.TAccessLog.Where(t => t.AccessLogId == obj.AccessLogId).FirstOrDefault();
        }

        public bool Insert(TAccessLog obj)
        {
            TAccessLog oResult = db.TAccessLog.Where(t => t.AccessLogId == obj.AccessLogId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.AccessLogCreatedDate = DateTime.Now;
            db.TAccessLog.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(TAccessLog obj)
        {
            TAccessLog oResult = db.TAccessLog.Where(t => t.AccessLogId == obj.AccessLogId).FirstOrDefault();
            if (oResult == null)
            {
                return false;
            }

            obj.AccessLogCreatedDate = DateTime.Now;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class NotificationLogRepository : BaseRepository, IRepository<TNotificationLog>
    {
        public bool Delete(TNotificationLog obj)
        {
            TNotificationLog oResult = db.TNotificationLog.Where(t => t.NotificationId == obj.NotificationId).FirstOrDefault();
            if (oResult == null)
            {
                return false;
            }

            oResult.UpdatedBy = 1;
            oResult.UpdatedDate = DateTime.Now;
            oResult.IsDeleted = 1;
            db.SaveChanges();

            return true;
        }

        public IQueryable<TNotificationLog> Get()
        {
            return db.TNotificationLog.AsQueryable<TNotificationLog>();
        }

        public TNotificationLog Get(TNotificationLog obj)
        {
            return db.TNotificationLog.Where(t => t.NotificationId == obj.NotificationId).FirstOrDefault();
        }

        public bool Insert(TNotificationLog obj)
        {
            TNotificationLog oResult = db.TNotificationLog.Where(t => t.NotificationId == obj.NotificationId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.TNotificationLog.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(TNotificationLog obj)
        {
            TNotificationLog oResult = db.TNotificationLog.Where(t => t.NotificationId == obj.NotificationId).FirstOrDefault();
            if (oResult == null)
            {
                return false;
            }

            obj.UpdatedBy = 1;
            obj.UpdatedDate = DateTime.Now;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
    }
}

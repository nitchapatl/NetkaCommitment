using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetkaCommitment.Repository
{
    public class FirebaseNotificationRepository : BaseRepository, IRepository<TFirebaseNotification>
    {
        public bool Delete(TFirebaseNotification obj)
        {
            if (!db.TFirebaseNotification.Any(t => t.FirebaseId == obj.FirebaseId))
            {
                return false;
            }

            obj.UpdatedBy = 1;
            obj.UpdatedDate = DateTime.Now;
            obj.IsDeleted = 1;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }

        public IQueryable<TFirebaseNotification> Get()
        {
            return db.TFirebaseNotification.AsQueryable<TFirebaseNotification>();
        }

        public TFirebaseNotification Get(TFirebaseNotification obj)
        {
            return db.TFirebaseNotification.Where(t => t.FirebaseId == obj.FirebaseId).FirstOrDefault();
        }

        public bool Insert(TFirebaseNotification obj)
        {
            TFirebaseNotification oResult = db.TFirebaseNotification.Where(t => t.FirebaseId == obj.FirebaseId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.TFirebaseNotification.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(TFirebaseNotification obj)
        {
            TFirebaseNotification oResult = db.TFirebaseNotification.Where(t => t.FirebaseId == obj.FirebaseId).FirstOrDefault();
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

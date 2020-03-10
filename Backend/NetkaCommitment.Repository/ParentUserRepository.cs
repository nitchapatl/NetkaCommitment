using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class ParentUserRepository : BaseRepository, IRepository<MParentUser>
    {
        public bool Delete(MParentUser obj)
        {
            if (!db.MParentUser.Any(t => t.ParentUserId == obj.ParentUserId))
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

        public IQueryable<MParentUser> Get()
        {
            return db.MParentUser.AsQueryable<MParentUser>();
        }

        public MParentUser Get(MParentUser obj)
        {
            return db.MParentUser.Where(t => t.ParentUserId == obj.ParentUserId).FirstOrDefault();
        }

        public bool Insert(MParentUser obj)
        {
            MParentUser oResult = db.MParentUser.Where(t => t.ParentUserId == obj.ParentUserId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.MParentUser.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(MParentUser obj)
        {
            MParentUser oResult = db.MParentUser.Where(t => t.ParentUserId == obj.ParentUserId).FirstOrDefault();
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

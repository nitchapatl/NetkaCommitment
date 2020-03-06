using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class UserRepository : BaseRepository, IRepository<MUser>
    {
        public bool Delete(MUser obj)
        {
            MUser oResult = db.MUser.Where(t => t.UserId == obj.UserId).FirstOrDefault();
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

        public IQueryable<MUser> Get()
        {
            return db.MUser.AsQueryable<MUser>();
        }

        public MUser Get(MUser obj)
        {
            return db.MUser.Where(t => t.UserId == obj.UserId).FirstOrDefault();
        }

        public bool Insert(MUser obj)
        {
            MUser oResult = db.MUser.Where(t => t.UserId == obj.UserId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.MUser.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(MUser obj)
        {
            MUser oResult = db.MUser.Where(t => t.UserId == obj.UserId).FirstOrDefault();
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

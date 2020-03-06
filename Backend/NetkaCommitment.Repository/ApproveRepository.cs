using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class ApproveRepository : BaseRepository, IRepository<TApprove>
    {

        public bool Delete(TApprove obj)
        {
            TApprove oResult = db.TApprove.Where(t => t.ApproveId == obj.ApproveId).FirstOrDefault();
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

        public IQueryable<TApprove> Get()
        {
            return db.TApprove.AsQueryable<TApprove>();
        }

        public TApprove Get(TApprove obj)
        {
            return db.TApprove.Where(t => t.ApproveId == obj.ApproveId).FirstOrDefault();
        }

        public bool Insert(TApprove obj)
        {
            TApprove oResult = db.TApprove.Where(t => t.ApproveId == obj.ApproveId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.TApprove.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(TApprove obj)
        {
            TApprove oResult = db.TApprove.Where(t => t.ApproveId == obj.ApproveId).FirstOrDefault();
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

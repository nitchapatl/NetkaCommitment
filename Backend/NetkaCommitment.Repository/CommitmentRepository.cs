using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class CommitmentRepository : BaseRepository, IRepository<TCommitment>
    {
        public bool Delete(TCommitment obj)
        {
            TCommitment oResult = db.TCommitment.Where(t => t.CommitmentId == obj.CommitmentId).FirstOrDefault();
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

        public IQueryable<TCommitment> Get()
        {
            return db.TCommitment.AsQueryable<TCommitment>();
        }

        public TCommitment Get(TCommitment obj)
        {
            return db.TCommitment.Where(t => t.CommitmentId == obj.CommitmentId).FirstOrDefault();
        }

        public bool Insert(TCommitment obj)
        {
            TCommitment oResult = db.TCommitment.Where(t => t.CommitmentId == obj.CommitmentId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.TCommitment.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(TCommitment obj)
        {
            TCommitment oResult = db.TCommitment.Where(t => t.CommitmentId == obj.CommitmentId).FirstOrDefault();
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

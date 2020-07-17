using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class CommitmentRepository : BaseRepository, IRepository<TCommitment>
    {
        public IQueryable<TCommitment> Get()
        {
            return db.TCommitment.AsQueryable<TCommitment>();
        }

        public TCommitment Get(TCommitment obj)
        {
            return db.TCommitment.Where(t => t.CommitmentId == obj.CommitmentId).FirstOrDefault();
        }
        public TCommitment GetCommitment(TCommitment obj) {
            return db.TCommitment.Where(x => x.CommitmentIsDeleted == 0 && x.CreatedBy==obj.CreatedBy).FirstOrDefault();
        }
        public bool Insert(TCommitment obj)
        {
            TCommitment oResult = db.TCommitment.Where(t => t.CommitmentId == obj.CommitmentId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            TCommitment oTCommitment = db.TCommitment.OrderByDescending(t => t.CommitmentId).FirstOrDefault();
            var lastId = !(oTCommitment == null) ? oTCommitment.CommitmentId+1 : 1;
            obj.CommitmentId = lastId; 
            obj.CommitmentNo = lastId;
            obj.CommitmentLm = obj.CommitmentLm; 
            obj.CommitmentName = obj.CommitmentName; 
            obj.UserId = 11;
            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            obj.IsDeleted = 0;
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
            obj.IsDeleted = 0;
            obj.CommitmentName = obj.CommitmentName;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
        public bool Delete(TCommitment obj)
        {
            if (!db.TCommitment.Any(t => t.CommitmentId == obj.CommitmentId))
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
    }
}

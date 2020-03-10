using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class DepartmentLmRepository : BaseRepository, IRepository<MDepartmentLm>
    {
        public bool Delete(MDepartmentLm obj)
        {
            if (!db.MDepartmentLm.Any(t => t.LmId == obj.LmId))
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

        public IQueryable<MDepartmentLm> Get()
        {
            return db.MDepartmentLm.AsQueryable<MDepartmentLm>();
        }

        public MDepartmentLm Get(MDepartmentLm obj)
        {
            return db.MDepartmentLm.Where(t => t.LmId == obj.LmId).FirstOrDefault();
        }

        public bool Insert(MDepartmentLm obj)
        {
            MDepartmentLm oResult = db.MDepartmentLm.Where(t => t.LmId == obj.LmId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.MDepartmentLm.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(MDepartmentLm obj)
        {
            MDepartmentLm oResult = db.MDepartmentLm.Where(t => t.LmId == obj.LmId).FirstOrDefault();
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

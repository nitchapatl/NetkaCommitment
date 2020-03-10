using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class DepartmentWigRepository : BaseRepository, IRepository<MDepartmentWig>
    {
        public bool Delete(MDepartmentWig obj)
        {
            if (!db.MDepartmentWig.Any(t => t.DepartmentWigId == obj.DepartmentWigId))
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

        public IQueryable<MDepartmentWig> Get()
        {
            return db.MDepartmentWig.AsQueryable<MDepartmentWig>();
        }

        public MDepartmentWig Get(MDepartmentWig obj)
        {
            return db.MDepartmentWig.Where(t => t.DepartmentWigId == obj.DepartmentWigId).FirstOrDefault();
        }

        public bool Insert(MDepartmentWig obj)
        {
            MDepartmentWig oResult = db.MDepartmentWig.Where(t => t.DepartmentWigId == obj.DepartmentWigId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.MDepartmentWig.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(MDepartmentWig obj)
        {
            MDepartmentWig oResult = db.MDepartmentWig.Where(t => t.DepartmentWigId == obj.DepartmentWigId).FirstOrDefault();
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

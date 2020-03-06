using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class DepartmentRepository : BaseRepository, IRepository<MDepartment>
    {
        public bool Delete(MDepartment obj)
        {
            MDepartment oResult = db.MDepartment.Where(t => t.DepartmentId == obj.DepartmentId).FirstOrDefault();
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

        public IQueryable<MDepartment> Get()
        {
            return db.MDepartment.AsQueryable<MDepartment>();
        }

        public MDepartment Get(MDepartment obj)
        {
            return db.MDepartment.Where(t => t.DepartmentId == obj.DepartmentId).FirstOrDefault();
        }

        public bool Insert(MDepartment obj)
        {
            MDepartment oResult = db.MDepartment.Where(t => t.DepartmentId == obj.DepartmentId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.MDepartment.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(MDepartment obj)
        {
            MDepartment oResult = db.MDepartment.Where(t => t.DepartmentId == obj.DepartmentId).FirstOrDefault();
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

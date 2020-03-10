using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class CompanyLmRepository : BaseRepository, IRepository<MCompanyLm>
    {
        public bool Delete(MCompanyLm obj)
        {
            if (!db.MCompanyLm.Any(t => t.CompanyLmId == obj.CompanyLmId))
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

        public IQueryable<MCompanyLm> Get()
        {
            return db.MCompanyLm.AsQueryable<MCompanyLm>();
        }

        public MCompanyLm Get(MCompanyLm obj)
        {
            return db.MCompanyLm.Where(t => t.CompanyLmId == obj.CompanyLmId).FirstOrDefault();
        }

        public bool Insert(MCompanyLm obj)
        {
            MCompanyLm oResult = db.MCompanyLm.Where(t => t.CompanyLmId == obj.CompanyLmId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.MCompanyLm.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(MCompanyLm obj)
        {
            MCompanyLm oResult = db.MCompanyLm.Where(t => t.CompanyLmId == obj.CompanyLmId).FirstOrDefault();
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

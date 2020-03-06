using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Linq;

namespace NetkaCommitment.Repository
{
    public class CompanyWigRepository<T> : BaseRepository, IRepository<MCompanyWig>
    {
        public bool Delete(MCompanyWig obj)
        {
            MCompanyWig oResult = db.MCompanyWig.Where(t => t.CompanyWigId == obj.CompanyWigId).FirstOrDefault();
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

        public IQueryable<MCompanyWig> Get()
        {
            return db.MCompanyWig.AsQueryable<MCompanyWig>();
        }

        public MCompanyWig Get(MCompanyWig obj)
        {
            return db.MCompanyWig.Where(t => t.CompanyWigId == obj.CompanyWigId).FirstOrDefault();
        }

        public bool Insert(MCompanyWig obj)
        {
            MCompanyWig oResult = db.MCompanyWig.Where(t => t.CompanyWigId == obj.CompanyWigId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            obj.UpdatedBy = null;
            obj.UpdatedDate = null;
            db.MCompanyWig.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(MCompanyWig obj)
        {
            MCompanyWig oResult = db.MCompanyWig.Where(t => t.CompanyWigId == obj.CompanyWigId).FirstOrDefault();
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

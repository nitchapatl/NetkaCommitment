using Microsoft.EntityFrameworkCore;
using NetkaCommitment.Data.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetkaCommitment.Repository
{
    public class AccessTokenRepository : BaseRepository, IRepository<TAccessToken>
    {
        public bool Delete(TAccessToken obj)
        {
            TAccessToken oResult = db.TAccessToken.Where(t => t.AccessTokenId == obj.AccessTokenId).FirstOrDefault();
            if (oResult == null)
            {
                return false;
            }

            db.Entry(obj).State = EntityState.Deleted;
            db.SaveChanges();

            return true;
        }

        public IQueryable<TAccessToken> Get()
        {
            return db.TAccessToken.AsQueryable<TAccessToken>();
        }

        public TAccessToken Get(TAccessToken obj)
        {
            return db.TAccessToken.Where(t => t.AccessTokenId == obj.AccessTokenId).FirstOrDefault();
        }

        public bool Insert(TAccessToken obj)
        {
            TAccessToken oResult = db.TAccessToken.Where(t => t.AccessTokenId == obj.AccessTokenId).FirstOrDefault();
            if (oResult != null)
            {
                return false;
            }

            obj.AccessTokenCreatedDate = DateTime.Now;
            obj.AccessTokenExpriedDate = DateTime.Now.AddMinutes(30);
            obj.AccessTokenUpdatedDate = null;
            db.TAccessToken.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Update(TAccessToken obj)
        {
            TAccessToken oResult = db.TAccessToken.Where(t => t.AccessTokenId == obj.AccessTokenId).FirstOrDefault();
            if (oResult == null)
            {
                return false;
            }

            obj.AccessTokenExpriedDate = DateTime.Now.AddMinutes(30);
            obj.AccessTokenUpdatedDate = DateTime.Now;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
    }
}

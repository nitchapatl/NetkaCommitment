using NetkaCommitment.Data.EFModels;
using NetkaCommitment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetkaCommitment.Business
{
    public class ApproveBiz : BaseBiz
    {
        private readonly IRepository<TApprove> oMainRepository = null;

        public ApproveBiz() {
            oMainRepository = new ApproveRepository();
        }

        public bool ApproveOrReject() {
            return oMainRepository.Insert(new TApprove
            {
                ApproveNo = (uint)db.TApprove.Where(t=>t.CommitmentId == 1).Count() + 1,
                ApproveType = "",
                ApproveStatus = "",
                ApproveRemark = "",
                CommitmentId = 1
            });
        }
    }
}

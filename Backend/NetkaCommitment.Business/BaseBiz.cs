using NetkaCommitment.Data.EFModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetkaCommitment.Business
{
    public class BaseBiz
    {
        protected NetkaCommitmentContext db = null;
        public BaseBiz()
        {
            db = new NetkaCommitmentContext();
        }
    }
}

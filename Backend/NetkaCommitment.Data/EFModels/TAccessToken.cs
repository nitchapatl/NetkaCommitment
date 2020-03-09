using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class TAccessToken
    {
        public uint AccessTokenId { get; set; }
        public string AccessTokenDevice { get; set; }
        public string AccessTokenKey { get; set; }
        public DateTime AccessTokenCreatedDate { get; set; }
        public DateTime? AccessTokenUpdatedDate { get; set; }
        public DateTime AccessTokenExpriedDate { get; set; }
        public uint? UserId { get; set; }

        public virtual MUser User { get; set; }
    }
}

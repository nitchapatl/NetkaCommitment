using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class TAccessLog
    {
        public uint AccessLogId { get; set; }
        public string AccessLogDevice { get; set; }
        public string AccessLogKey { get; set; }
        public string AccessLogUrl { get; set; }
        public DateTime AccessLogCreatedDate { get; set; }
        public uint UserId { get; set; }

        public virtual MUser User { get; set; }
    }
}

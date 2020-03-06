using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class TNotificationLog
    {
        public uint NotificationId { get; set; }
        public string NotificationName { get; set; }
        public string NotificationDescription { get; set; }
        public string NotificationFcmKey { get; set; }
        public string NotificationPlatform { get; set; }
        public ulong NotificationIsSent { get; set; }
        public uint NotificationCount { get; set; }
        public uint NotificationLimit { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public uint UserId { get; set; }

        public virtual MUser User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class TFirebaseNotification
    {
        public uint FirebaseId { get; set; }
        public string FirebaseMacAddress { get; set; }
        public string FirebaseFcmKey { get; set; }
        public string FirebasePlatform { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public uint UserId { get; set; }

        public virtual MUser User { get; set; }
    }
}

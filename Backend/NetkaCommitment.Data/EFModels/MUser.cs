using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class MUser
    {
        public MUser()
        {
            MParentUserParent = new HashSet<MParentUser>();
            MParentUserUser = new HashSet<MParentUser>();
            TFirebaseNotification = new HashSet<TFirebaseNotification>();
            TNotificationLog = new HashSet<TNotificationLog>();
        }

        public uint UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserFirstNameEn { get; set; }
        public string UserLastNameEn { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public ulong IsDeleted { get; set; }
        public uint? DepartmentId { get; set; }

        public virtual MDepartment Department { get; set; }
        public virtual ICollection<MParentUser> MParentUserParent { get; set; }
        public virtual ICollection<MParentUser> MParentUserUser { get; set; }
        public virtual ICollection<TFirebaseNotification> TFirebaseNotification { get; set; }
        public virtual ICollection<TNotificationLog> TNotificationLog { get; set; }
    }
}

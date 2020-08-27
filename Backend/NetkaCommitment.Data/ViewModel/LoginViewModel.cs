using System;
using System.Collections.Generic;
using System.Text;

namespace NetkaCommitment.Data.ViewModel
{
    public class LoginViewModel
    {

    }

    public class LoginUserViewModel {
        public uint UserId { get; set; }
        public string UserCode { get; set; }
        public string UserDevice { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserFirstNameEn { get; set; }
        public string UserLastNameEn { get; set; }
        public uint? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }


    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }


    public class ResetPasswordViewModel
    {
        public string UserName { get; set; }

        public string OTP { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}

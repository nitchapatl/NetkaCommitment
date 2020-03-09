using NetkaCommitment.Common;
using NetkaCommitment.Data.EFModels;
using NetkaCommitment.Data.ViewModel;
using NetkaCommitment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetkaCommitment.Business
{
    public class LoginBiz : BaseBiz
    {
        private readonly UserRepository oUserRepository = null;
        public LoginBiz()
        {
            oUserRepository = new UserRepository();
        }

        public bool AuthorizeExist(LoginUserViewModel model)
        {
            return oUserRepository.Get().Any(t => t.UserName == model.UserName && t.IsDeleted == 0);
        }

        public string Authorize(LoginUserViewModel model)
        {
            MUser oUser = oUserRepository.Get().Where(t => t.UserName == model.UserName && t.IsDeleted == 0 && t.UserPassword == model.UserPassword.Trim()).FirstOrDefault();
            string strToken = null;
            if (oUser != null)
            {
                strToken = EncryptionHelpers.GenerateString();
                var oDevice = oAccessTokenRepository.Get().Where(t => t.AccessTokenDevice == model.UserDevice).FirstOrDefault();
                if (oDevice == null)
                {
                    oAccessTokenRepository.Insert(new TAccessToken
                    { 
                        AccessTokenDevice = model.UserDevice,
                        AccessTokenKey = strToken,
                        AccessTokenCreatedDate = DateTime.Now,
                        AccessTokenExpriedDate = DateTime.Now.AddMinutes(30),
                        UserId = oUser.UserId
                    });
                }
                else 
                {
                    oDevice.AccessTokenKey = strToken;
                    oDevice.AccessTokenUpdatedDate = DateTime.Now;
                    oDevice.AccessTokenExpriedDate = DateTime.Now.AddMinutes(30);
                    oAccessTokenRepository.Update(oDevice);
                }

                oAccessLogRepository.Insert(new TAccessLog
                {
                    AccessLogDevice = model.UserDevice,
                    AccessLogKey = strToken,
                    AccessLogUrl = HTTPHelpers.HttpContext.Request.Path.ToString(),
                    AccessLogCreatedDate = DateTime.Now,
                    UserId = oUser.UserId
                });
            }
            return strToken;
        }
        public bool ForgotPassword(LoginUserViewModel model)
        {
            var result = oUserRepository.Get().Where(t => t.UserName == model.UserName && t.IsDeleted == 0).FirstOrDefault();

            if (result != null)
            {
                result.UserPasswordResetToken = EncryptionHelpers.GenerateString();

                // Email Sender

                return oUserRepository.Update(result);
            }

            return false;
        }

        public LoginUserViewModel GetUser(int id)
        {
            LoginUserViewModel oUser = oUserRepository.Get().Where(t => t.UserId == id && t.IsDeleted == 0).Select(t => new LoginUserViewModel
            {
                UserId = t.UserId,
                UserCode = t.UserCode,
                UserName = t.UserName,
                //UserPassword = t.UserPassword,
                UserFirstName = t.UserFirstName,
                UserLastName = t.UserLastName,
                UserFirstNameEn = t.UserFirstNameEn,
                UserLastNameEn = t.UserLastNameEn,
                DepartmentId = t.Department == null ? (uint?)null : t.Department.DepartmentId,
                DepartmentName = t.Department == null ? null : t.Department.DepartmentName
            }).FirstOrDefault();

            return oUser;
        }
    }
}

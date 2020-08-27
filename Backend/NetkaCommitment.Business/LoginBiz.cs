using NetkaCommitment.Common;
using NetkaCommitment.Data.EFModels;
using NetkaCommitment.Data.ViewModel;
using NetkaCommitment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Policy;

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

        public AuthorizeTokenViewModel Authorize(LoginUserViewModel model, IHttpContextAccessor httpcontext)
        {
            MUser oUser = oUserRepository.Get().Where(t => t.UserName == model.UserName && t.IsDeleted == 0 && t.UserPassword == model.UserPassword.Trim()).FirstOrDefault();
            string strToken = null;
            long userId = 0;
            if (oUser != null)
            {
                //strToken = EncryptionHelpers.GenerateString();
                strToken = GenerateJWTToken(oUser);
                userId = oUser.UserId;
                var oDevice = oAccessTokenRepository.Get().Where(t => t.AccessTokenDevice == model.UserDevice).FirstOrDefault();
                if (oDevice == null)
                {
                    oAccessTokenRepository.Insert(new TAccessToken
                    {
                        AccessTokenDevice = model.UserDevice,
                        AccessTokenKey = strToken,
                        AccessTokenCreatedDate = DateTime.Now,
                        AccessTokenExpriedDate = DateTime.Now.AddMinutes(JwtHelpers.JwtExpired),
                        UserId = oUser.UserId
                    });
                }
                else
                {
                    oDevice.AccessTokenKey = strToken;
                    oDevice.AccessTokenUpdatedDate = DateTime.Now;
                    oDevice.AccessTokenExpriedDate = DateTime.Now.AddMinutes(JwtHelpers.JwtExpired);
                    oAccessTokenRepository.Update(oDevice);
                }

                oAccessLogRepository.Insert(new TAccessLog
                {
                    AccessLogDevice = model.UserDevice,
                    AccessLogKey = strToken,
                    AccessLogUrl = httpcontext.HttpContext.Request.Path.ToString(),
                    AccessLogCreatedDate = DateTime.Now,
                    UserId = oUser.UserId
                });
            }

            //return strToken;

            return new AuthorizeTokenViewModel
            {
                Id = userId,
                Token = strToken,
                Status = ""
            };
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

        public string ForgotPassword(ForgotPasswordViewModel model)
        {
            var result = oUserRepository.Get().Where(t => t.UserName == model.UserName && t.IsDeleted == 0).FirstOrDefault();

            if (result != null)
            {

                result.UserPasswordResetToken = EncryptionHelpers.GeneratePasswordOTP();

                oUserRepository.Update(result);

                return result.UserPasswordResetToken;
            }

            return "";
        }

        public string ResetPassword(ResetPasswordViewModel model)
        {
            var User = oUserRepository.Get().Where(t => t.UserName == model.UserName && t.IsDeleted == 0).FirstOrDefault();
            string result = "";
            if (User != null)
            {
                var UserOTP = oUserRepository.Get().Where(t => t.UserId == User.UserId && t.UserPasswordResetToken == model.OTP).FirstOrDefault();

                if (UserOTP != null)
                {
                    UserOTP.UserPassword = model.NewPassword;
                    UserOTP.UserPasswordResetToken = null;

                    oUserRepository.Update(UserOTP);

                    result = "Reset password success";
                }
                else
                {
                    result = "Invalid OTP";
                }
            }
            else
            {
                result = "Username not found";
            }

            return result;
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

        public LoginUserViewModel GetUser(string username)
        {
            LoginUserViewModel oUser = oUserRepository.Get().Where(t => t.UserName == username && t.IsDeleted == 0).Select(t => new LoginUserViewModel
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

        private string GenerateJWTToken(MUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtHelpers.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(JwtHelpers.JwtExpired);
            var claims = new[]
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("fullName", userInfo.UserFirstName.ToString() + " " + userInfo.UserLastName.ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
                                issuer: JwtHelpers.JwtIssuer,
                                audience: JwtHelpers.JwtAudience,
                                claims: claims,
                                expires: expires,
                                signingCredentials: credentials
                               );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

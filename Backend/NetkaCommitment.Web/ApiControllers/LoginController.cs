using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetkaCommitment.Business;
using NetkaCommitment.Data.ViewModel;

namespace NetkaCommitment.Web.ApiControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseApiController
    {

        private readonly LoginBiz oLoginBiz = null;
        private readonly IHttpContextAccessor _httpcontext;

        public LoginController(IHttpContextAccessor oHttpContextAccessor) : base(oHttpContextAccessor) { 
            oLoginBiz = new LoginBiz();
            _httpcontext = oHttpContextAccessor;
        }

        //[AllowAnonymous]
        [HttpPost("getuser/{UserID}")]
        public IActionResult GetUser(int UserId)
        {
            var result = oLoginBiz.GetUser(UserId);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [AllowAnonymous]
        [HttpPost("autorize")]
        public IActionResult Autorize([FromBody] LoginUserViewModel model)
        {
            var strToken = oLoginBiz.Authorize(model, _httpcontext);
            //if (!string.IsNullOrEmpty(strToken))
            if (strToken != null)
            {
                return StatusCode(StatusCodes.Status201Created, strToken);
            }
            else if (oLoginBiz.AuthorizeExist(model))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Wrong password.");
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "Username not found");
            }
        }

        [AllowAnonymous]
        [HttpPost("authorize")]
        public IActionResult Authorize([FromBody] LoginUserViewModel model)
        {
            AuthorizeTokenViewModel strToken = oLoginBiz.Authorize(model, _httpcontext);
            if (!string.IsNullOrEmpty(strToken.Token))
            {
                strToken.Status = "Login success";
                return StatusCode(StatusCodes.Status201Created, strToken);
            }
            else {
                if (oLoginBiz.AuthorizeExist(model))
                {
                    strToken.Status = "Wrong password";
                    return StatusCode(StatusCodes.Status400BadRequest, strToken.Status);
                }
                else
                {
                    strToken.Status = "Username not found";
                    return StatusCode(StatusCodes.Status404NotFound, strToken.Status);
                }
            }
        }

        /*[AllowAnonymous]
        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword([FromBody] LoginUserViewModel model)
        {
            if (oLoginBiz.ForgotPassword(model))
            {
                return StatusCode(StatusCodes.Status205ResetContent);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "Username not found");
            }
        }*/

        [AllowAnonymous]
        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            var user = oLoginBiz.GetUser(model.UserName);

            // Email

            var UserPasswordResetToken = oLoginBiz.ForgotPassword(model);

            /*var link = Url.Action("resetpassword", "login",
                                  new { userId = user.UserId, token = UserPasswordResetToken }, Request.Scheme);*/

            if (UserPasswordResetToken != "")
            {
                sendResetPasswordEmail(model.Email, UserPasswordResetToken);
                return StatusCode(StatusCodes.Status201Created, UserPasswordResetToken);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "Username not found");
            }
        }

        public void sendResetPasswordEmail(string Email, string UserResetPasswordOTP)
        {
            // Credentials
            var credentials = new NetworkCredential("netkacommitment@gmail.com", "netka123");

            // Mail message
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("noreply@netkacommitment.com");
            mail.To.Add(Email);
            mail.Subject = "Netkacommitment Reset Password";
            mail.Body = "<br> Your reset password OTP: " + UserResetPasswordOTP;
            mail.IsBodyHtml = true;

            // Smtp client
            var client = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };
            client.Send(mail);

        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            
            var result = oLoginBiz.ResetPassword(model);

            if (result == "Reset password success")
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else if (result == "Invalid OTP")
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, result);
            }
        }

    }
}

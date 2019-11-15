using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;


namespace Bug_Tracker.Helpers
{
    public class EmailHelper
    {
        private static string ConfiguredEmail = WebConfigurationManager.AppSettings["emailto"];
        
        public static async Task ComposeEmailAsync(Models.EmailModel email) 
        {
            try 
            {
                var senderEmail = $"{email.FromEmail}<{ConfiguredEmail}>";  
                var mailMsg = new MailMessage(senderEmail, ConfiguredEmail)
                {
                    Subject = email.Subject,
                    Body = $"<strong>{email.FromName} has sent you the following message</strong><hr/r> {email.Body}",
                    IsBodyHtml = true
                };
                var svc = new EmailService();
                await svc.SendAsync(mailMsg);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }
        }

        public static async Task ComposeEmailAsync(RegisterViewModel model, string callbackUrl) 
        {
            try 
            {
                var senderEmail = $"Admin<{ConfiguredEmail}>";
                var mailMsg = new MailMessage(senderEmail, model.Email)
                {
                    Subject = "Confirm your account",
                    Body = $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>",
                    IsBodyHtml = true
                };

                var svc = new EmailService();
                await svc.SendAsync(mailMsg);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }
            
        }

        public static async Task ComposeEmailAsync(ForgotPasswordViewModel model, string callbackUrl) 
        {
            try 
            {
                var senderEmail = $"Admin<{ConfiguredEmail}>";
                var mailMsg = new MailMessage(senderEmail, model.Email)
                {
                    Subject = "Reset Password",
                    Body = $"Please reset your password by clicking <a href=\"{callbackUrl}\">here</a>",
                    IsBodyHtml = true
                };

                var svc = new EmailService();
                await svc.SendAsync(mailMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }
        }

    }
}
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace WebAPIScheduledTasksWithQuartz.Scheduled
{
    public class EmailJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var _email = "frommail";
                var _epass = "yourpassword";
                var _dispName = "Scheduled test mail";
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add("tomail");
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = "Scheduled subject";
                myMessage.Body = "Scheduled message";
                myMessage.IsBodyHtml = true;


                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.yandex.com.tr";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
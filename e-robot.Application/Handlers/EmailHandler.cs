using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace e_robot.Application.Handlers
{
    public class EmailHandler
    {
        public static void SendEmail(string to, string subject,string html)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("code@robosolutions.tk"); // Адрес отправителя

            mail.To.Add(to);
            mail.Subject = subject;

            var plainView = AlternateView.CreateAlternateViewFromString(html, null, "text/plain");
            var htmlView = AlternateView.CreateAlternateViewFromString(html, null, "text/html");
            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);

            SmtpClient client = new SmtpClient();
            client.Host = "robosolutions.tk";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("code@robosolutions.tk", "8*j3bN0n"); // Ваши логин и пароль
            client.Send(mail);
        }
    }
}

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


using System.Threading;

namespace DeepakAPI.Utility.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public bool SendEmail(string body, Dictionary<string, string> replacements, string subject, string toEmail, string bccEmail)
        {
            var builder = new ConfigurationBuilder()  //Need Nuget Package for using this function (Microsoft.Extension.Configuration)
                .AddJsonFile("appsettings.json", true, true);

            IConfigurationRoot configuration = builder.Build();

            replacements.ToList().ForEach(x =>
            {
                body = body.Replace(x.Key, Convert.ToString(x.Value));
            });
            // Plug in your email service here to send an email.
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(configuration.GetSection("EmailCrednetials").GetSection("From").Value, configuration.GetSection("EmailCrednetials").GetSection("From").Value));
            msg.To.Add(new MailboxAddress(toEmail));


            if (!string.IsNullOrEmpty(bccEmail))
                msg.Bcc.Add(new MailboxAddress(bccEmail));

            msg.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            msg.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())  //need to install nuget Package NETCore.Mailkit for sending email
            {
                smtp.Connect(configuration.GetSection("EmailCrednetials").GetSection("Host").Value, Convert.ToInt16(configuration.GetSection("EmailCrednetials").GetSection("Port").Value), SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(credentials: new NetworkCredential(configuration.GetSection("EmailCrednetials").GetSection("UserName").Value, configuration.GetSection("EmailCrednetials").GetSection("Password").Value));
                smtp.Send(msg, CancellationToken.None);
                smtp.Disconnect(true, CancellationToken.None);
            }
            return true;
        }
    }
}

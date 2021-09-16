using CrossCutting.ApiModel;
using FileManger.Utils.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace FileManger.Utils
{
    public class SendEmails : ISendEmails
    {
        public IConfiguration Configuration { get; }
        private static readonly string AFFAIR = "Seguridad - UA";
        private readonly string fromAddress;
        private readonly string fromName;
        private readonly string fromPassword;
        private readonly string host;
        private readonly int port;

        public SendEmails(IConfiguration Conf)
        {
            Configuration = Conf;
            host = Configuration["EmailSettings:PrimaryDomain"];
            fromName = Configuration["EmailSettings:FromEmail"];
            fromAddress = Configuration["EmailSettings:UsernameEmail"];
            fromPassword = Configuration["EmailSettings:UsernamePassword"];
            port = Convert.ToInt32(Configuration["EmailSettings:PrimaryPort"]);
        }

        public Boolean SendEmailConfig(EmailAM emailData)
        {
            string emailBody = "<p>Respetado<p>" + "<p>Usuario</p> <br> <p>" + emailData.Message + "</p><br>";

            emailBody += "<p>Cordialmente,</p> <br> </p>UA</p>";

            return SendEmail(emailData.Email, AFFAIR, emailBody);
        }

        private Boolean SendEmail(string to, string subject, string body)
        {
            try
            {
                var frAddress = new MailAddress(fromAddress, fromName);
                var toAddress = new MailAddress(to, to);

                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(frAddress.Address, fromPassword),
                    EnableSsl = true,
                };

                using (var message = new MailMessage(frAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}

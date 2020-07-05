using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Top_Records.Models
{
    public class EmailData
    {
        private static string Username { get; set; }
        private static string Password { get; set; }
        private static int Port { get; set; }
        private static bool Ssl { get; set; }
        private static string Host { get; set; }
        private static string confirmURL { get; set; }

        public EmailData(IConfiguration _config)
        {
            Username = _config.GetSection("EmailData:Username").Value;
            Password = _config.GetSection("EmailData:Password").Value;
            Host = _config.GetSection("EmailData:Host").Value;
            Port = System.Convert.ToInt32(_config.GetSection("EmailData:Port").Value.ToString());
            Ssl = System.Convert.ToBoolean(_config.GetSection("EmailData:Ssl").Value);            
        }

        public EmailData(Record record, string view, string baseURL)
        {
            if (!record.Approved)
            {
                try
                {
                    confirmURL = baseURL;

                    var Login = new NetworkCredential(Username, Password);

                    var Client = new SmtpClient(Host, Port);
                    Client.UseDefaultCredentials = false;
                    Client.Credentials = Login;
                    Client.EnableSsl = Ssl;
                    Client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    var Message = new MailMessage(from: Login.UserName, to: record.Email);
                    Message.Subject = "Top Records vrijeme";

                    if (view == "UnapprovedRecords")
                    {
                        Message.Body = "Pozdrav " + record.Name + " " + record.Surname + ", <br /><br />" +
                         " ovim putem Vas obavještavamo da je Vaše prijavljeno vrijeme: '" + record.Time + "' odbijeno sa strane administratora. <br />" +
                          "<html><head></head><body> Za potvrdu <a href=" + confirmURL + ">kliknite ovdje</a></body></html><br /><br />" +
                         "Lijep pozdrav, <br /> " +
                         "TopRecords";
                    }
                    else if (view == "Index")
                    {
                        Message.Body = "Pozdrav " + record.Name + " " + record.Surname + ", <br /><br />" +
                         " ovim putem Vas obavještavamo da je Vaše prijavljeno vrijeme: '" + record.Time + "' obrisano sa Top Records liste. <br />" +
                         "<html><head></head><body> Za potvrdu <a href=" + confirmURL + ">kliknite ovdje</a></body></html><br /><br />" +
                         "Lijep pozdrav, <br /> " +
                         "TopRecords";
                    }
                    Message.BodyEncoding = Encoding.UTF8;
                    Message.IsBodyHtml = true;
                    Message.Priority = MailPriority.Normal;
                    Client.SendAsync(Message, "");
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
    }
}

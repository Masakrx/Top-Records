using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Top_lista_vremena.Models
{
    public class EmailData
    {
        protected  NetworkCredential Login { get; set; }
        protected SmtpClient Client { get; set; }
        protected MailMessage Message { get; set; }

        public EmailData(Record record)
        {
            Login = new NetworkCredential("TopRecordsApp@outlook.com", "toprecords123");

            Client = new SmtpClient("smtp.office365.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = Login;

            Message = new MailMessage(from: Login.UserName, to: record.Email);
            Message.Subject = "Prijava vremena";
            Message.Body = "Pozdrav "+record.Name+" "+record.Surname+ ", <br /><br />" +
                " ovim putem Vas obavještavamo da je Vaše vrijeme odobreno i uneseno na top listu sa strane administratora. <br /><br />" +
                "Lijep pozdrav, <br /> " +
                "TopRecordsApp";
            Message.IsBodyHtml = true;
            Message.Priority = MailPriority.Normal;
            Client.SendAsync(Message, "");
        }
    }
}

using System.Net;
using System.Net.Mail;
using System.Text;

namespace Top_lista_vremena.Models
{
    public class EmailData
    {
        public static NetworkCredential Login { get; set; }
        public static SmtpClient Client { get; set; }
        public static MailMessage Message { get; set; }

        public EmailData(Record record, string view)
        {
            try
            {
                Login = new NetworkCredential("TopRecordsApp@yahoo.com", "toprecords123");
                //Login = new NetworkCredential("TopRecordsApp@outlook.com", "toprecords123");

                Client = new SmtpClient("smtp.mail.yahoo.com", 587);
                //Client = new SmtpClient("smtp.office365.com", 587);
                Client.UseDefaultCredentials = false;
                Client.Credentials = Login;
                Client.EnableSsl = true;
                Client.DeliveryMethod = SmtpDeliveryMethod.Network;


                Message = new MailMessage(from: Login.UserName, to: record.Email);
                Message.Subject = "Top Records vrijeme";

                if (record.Approved)
                {
                    Message.Body = "Pozdrav " + record.Name + " " + record.Surname + ", <br /><br />" +
                    " ovim putem Vas obavještavamo da je Vaše prijavljeno vrijeme: '" + record.Time + "' odobreno i uneseno na top listu. <br /><br />" +
                    "Lijep pozdrav, <br /> " +
                    "TopRecordsApp";
                }
                else if (!record.Approved && view == "UnapprovedRecords")
                {
                    Message.Body = "Pozdrav " + record.Name + " " + record.Surname + ", <br /><br />" +
                     " ovim putem Vas obavještavamo da je Vaše prijavljeno vrijeme: '" + record.Time + "' odbijeno sa strane administratora. <br /><br />" +
                     "Lijep pozdrav, <br /> " +
                     "TopRecordsApp";
                }
                else
                {
                    Message.Body = "Pozdrav " + record.Name + " " + record.Surname + ", <br /><br />" +
                     " ovim putem Vas obavještavamo da je Vaše prijavljeno vrijeme: '" + record.Time + "' obrisano sa Top Records liste. <br /><br />" +
                     "Lijep pozdrav, <br /> " +
                     "TopRecordsApp";
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

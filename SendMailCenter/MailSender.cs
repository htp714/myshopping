using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
namespace SendMailCenter
{
    public class MailSender
    {
        MailAddress fromAddress = new MailAddress("fatenopolis@gmail.com", "From Name");
        MailAddress toAddress = new MailAddress("htp714@gmail.com", "To Name");
        const string fromPassword = "lordearon";
       

        public void Send(string name, string email, string subject, string message)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var tomessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = message
            })
            {
                smtp.Send(tomessage);
            }
        }
    }
}

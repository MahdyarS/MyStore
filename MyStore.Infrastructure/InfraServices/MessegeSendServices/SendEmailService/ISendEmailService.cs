using MyStore.Common.InfraStructureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.InfraServices.MessegeSendServices.SendEmailService
{
    public interface ISendEmailService:ISendMessegeService
    {
    }


    public class SendGmail : ISendEmailService
    {
        public Task SendMessage(MessageDto message)
        {
            SmtpClient server = new SmtpClient();
            server.Host = "smtp.gmail.com";
            server.Port = 587;
            server.EnableSsl = true;
            server.DeliveryMethod = SmtpDeliveryMethod.Network;
            server.UseDefaultCredentials = false;
            server.Credentials = new NetworkCredential("alirezayi2342@gmail.com","Assassins100%");

            MailMessage toSend = new MailMessage(message.From,message.To,message.Subject,message.Body);
            toSend.IsBodyHtml = true;
            toSend.BodyEncoding = UTF8Encoding.UTF8;
            toSend.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            server.Send(toSend);

            return Task.CompletedTask;
        }
    }

}

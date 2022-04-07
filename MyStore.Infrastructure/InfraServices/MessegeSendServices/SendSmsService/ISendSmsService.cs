using MyStore.Common.InfraStructureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.InfraServices.MessegeSendServices.SendSmsService
{
    public interface ISendSmsService:ISendMessegeService
    {
    }

    public class SendSmsService : ISendSmsService
    {
        public Task SendMessage(MessageDto message)
        {
            var server = new WebClient();
            string url = "";
            var content = server.DownloadString(url);

            return Task.CompletedTask;
        }
    }
}

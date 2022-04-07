using MyStore.Common.InfraStructureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Interfaces.SendMessege
{
    public interface ISendMessegeInterface
    {
        Task SendMessage(MessageDto message);
    }
}

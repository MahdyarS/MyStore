using Microsoft.AspNetCore.SignalR;
using MyStore.Application.Services.VisitorServices.OnlineVisitorsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endpoint.Site.Hubs
{
    public class OnlineUsersHub:Hub
    {
        private readonly IOnlineVisitorService _onlineVisitorService;

        public OnlineUsersHub(IOnlineVisitorService onlineVisitorService)
        {
            _onlineVisitorService = onlineVisitorService;
        }

        public override Task OnConnectedAsync()
        {
            string visitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _onlineVisitorService.ConnectNewVisitor(visitorId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string visitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _onlineVisitorService.DisConnectVisitor(visitorId);
            return base.OnDisconnectedAsync(exception);
        }

    }
}

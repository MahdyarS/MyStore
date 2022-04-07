using Microsoft.AspNetCore.Mvc.Filters;
using MyStore.Application.Services.VisitorServices.SaveVisitorInfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAParser;

namespace Endpoint.Site.Helpers.Filters
{
    public class GetVisitorInfoActionFilter : IActionFilter
    {
        private readonly ISaveVisitorsInfoService _saveVisitorInfoService;

        public GetVisitorInfoActionFilter(ISaveVisitorsInfoService saveVisitorInfoService)
        {
            _saveVisitorInfoService = saveVisitorInfoService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string ip = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var currentLink = context.HttpContext.Request.Path;
            var refererLink = context.HttpContext.Request.Headers["Referer"].ToString();
            var protocol = context.HttpContext.Request.Protocol;
            var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            string userId = context.HttpContext.Request.Cookies["VisitorId"];
            var method = context.HttpContext.Request.Method;
            var uaParser = Parser.GetDefault();
            ClientInfo clientInfo = uaParser.Parse(userAgent);
            var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            var physicalPath = $"{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}/{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}";
            string visitorId = context.HttpContext.Request.Cookies["VisitorId"];

            _saveVisitorInfoService.Execute(new SaveVisitorInfoRequestDto
            {
                Ip = ip,
                CurrentLink = currentLink,
                RefererLink = refererLink,
                Protool = protocol,
                Method = method,
                PhysicalPath = physicalPath,
                Browser = new VisitorsSoftwareInfoDto
                {
                    Name = clientInfo.UA.Family,
                    Version = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}"
                },
                Device = new VisitorsDeviceDto
                {
                    Name = clientInfo.Device.Family,
                    Model = clientInfo.Device.Model,
                    IsSpider = clientInfo.Device.IsSpider,
                    Brand = clientInfo.Device.Brand
                },
                OS = new VisitorsSoftwareInfoDto
                {
                    Name = clientInfo.OS.Family,
                    Version = $"{clientInfo.OS.Major}.{clientInfo.OS.Minor}.{clientInfo.OS.Patch}.{clientInfo.OS.PatchMinor}"
                },
                Time = DateTime.Now,
                VisitorId = visitorId
            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}

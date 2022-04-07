using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UtilityServices.UrlGeneratorService
{
    public interface IUrlGeneratorService
    {
        string CreateUrl(string action, string controller,object values = null, string area = null);
    }
    public class UrlGeneratorService : IUrlGeneratorService
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActionContextAccessor _actionContextAccessor;

        public UrlGeneratorService(IUrlHelperFactory urlHelperFactory,
                                   IHttpContextAccessor httpContextAccessor,
                                   IActionContextAccessor actionContextAccessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _httpContextAccessor = httpContextAccessor;
            _actionContextAccessor = actionContextAccessor;
        }

        public string CreateUrl(string action, string controller, object values = null, string area = "")
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            return urlHelper.Action(action, controller, values, protocol: _httpContextAccessor.HttpContext.Request.Scheme);
        }
    }
}

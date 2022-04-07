using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endpoint.Site.Areas.Admin.Models.ViewModels.UsersViewModels
{
    public class UsersListRequestViewModel
    {
        public string SearchKey { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPage { get; set; } = 1;

    }
}

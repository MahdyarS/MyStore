using MyStore.Application.Services.VisitorServices.GenerateGeneralVisitorsReportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UtilityServices.MultiServicePageServices.AdminHomePageService
{
    public interface IAdminHomePageService
    {
        AdminHomePageDataDto Execute();
    }

    public class AdminHomePageService : IAdminHomePageService
    {
        private readonly IGenerateGeneralVisitorsReportService _generateGeneralVisitorsReportService;

        public AdminHomePageService(IGenerateGeneralVisitorsReportService generateGeneralVisitorsReportService)
        {
            _generateGeneralVisitorsReportService = generateGeneralVisitorsReportService;
        }

        public AdminHomePageDataDto Execute()
        {
            var result = new AdminHomePageDataDto();

            result.VisitsReport = _generateGeneralVisitorsReportService.Execute();

            return result;

        }
    }

    public class AdminHomePageDataDto
    {
        public GeneralVisitsReport VisitsReport { get; set; }
    }
}

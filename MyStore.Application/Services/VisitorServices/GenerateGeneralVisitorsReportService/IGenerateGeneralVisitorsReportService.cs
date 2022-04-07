using MongoDB.Driver;
using MyStore.Application.Interfaces.Contexts;
using MyStore.Application.Services.VisitorServices.GetLastMonthVisitorsReportService;
using MyStore.Application.Services.VisitorServices.GetTodayVisitsReportService;
using MyStore.Application.Services.VisitorServices.GetTotalVisitorsReportService;
using MyStore.Domain.Entities.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.VisitorServices.GenerateGeneralVisitorsReportService
{
    public interface IGenerateGeneralVisitorsReportService
    {
        GeneralVisitsReport Execute();
    }

    public class GenerateGeneralVisitorsReportService : IGenerateGeneralVisitorsReportService
    {
        private readonly IGetLastMonthVisitorsReportService _getLastMonthVisitorsReportService;
        private readonly IGetTotalVisitorsReportService _getTotalVisitorsReportService;
        private readonly IGetTodayVisitsReportService _getTodayVisitsReportService;

        public GenerateGeneralVisitorsReportService(IGetLastMonthVisitorsReportService getLastMonthVisitorsReportService,
                                                    IGetTotalVisitorsReportService getTotalVisitorsReportService,
                                                    IGetTodayVisitsReportService getTodayVisitsReportService)
        {
            _getLastMonthVisitorsReportService = getLastMonthVisitorsReportService;
            _getTotalVisitorsReportService = getTotalVisitorsReportService;
            _getTodayVisitsReportService = getTodayVisitsReportService;
        }


        public GeneralVisitsReport Execute()
        {
            return new GeneralVisitsReport()
            {
                LastMonthsReport = _getLastMonthVisitorsReportService.Execute(),
                TotalReport = _getTotalVisitorsReportService.Execute(),
                TodaysReport = _getTodayVisitsReportService.Execute()
            };
        }
    }

    public class GeneralVisitsReport
    {
        public TodayVisitsReportDto TodaysReport { get; set; }
        public LastMonthVisitsReportDto LastMonthsReport { get; set; }
        public TotalVisitsReportDto TotalReport { get; set; }
    }

}

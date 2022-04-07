using MongoDB.Driver;
using MyStore.Application.Interfaces.Contexts;
using MyStore.Application.Services.VisitorServices.OnlineVisitorsService;
using MyStore.Domain.Entities.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStore.Common.Services.SetPrecision;

namespace MyStore.Application.Services.VisitorServices.GetTodayVisitsReportService
{
    public interface IGetTodayVisitsReportService
    {
        TodayVisitsReportDto Execute();
    }
    public class GetTodayVisitsReportService: IGetTodayVisitsReportService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> _mongoCollection;
        private readonly IOnlineVisitorService _onlineVisitorService;


        public GetTodayVisitsReportService(IMongoDbContext<Visitor> mongoDbContext, IOnlineVisitorService onlineVisitorService)
        {
            _mongoDbContext = mongoDbContext;
            _onlineVisitorService = onlineVisitorService;
            _mongoCollection = _mongoDbContext.GetCollections();
        }


        public TodayVisitsReportDto Execute()
        {
            var result = new TodayVisitsReportDto();

            var TodaysBeginning = DateTime.Now.Date;
            var TodaysEnd = DateTime.Now.Date.AddDays(1);

            result.VisitsCount = _mongoCollection.AsQueryable()
                                                .Where(p => p.Time >= TodaysBeginning)
                                                .LongCount();

            result.VisitorsCount = _mongoCollection.AsQueryable()
                                                .Where(p => p.Time >= TodaysBeginning)
                                                .GroupBy(p => p.VisitorId)
                                                .LongCount();

            if (result.VisitorsCount == 0)
                result.VisitsPerVisitor = 0;
            else
                result.VisitsPerVisitor = ((double)result.VisitsCount) / result.VisitorsCount;

            result.VisitsPerVisitor = result.VisitsPerVisitor.SetPrecision(2);

            result.OnlineVisitorsCount = _onlineVisitorService.GetOnlineUsersCount();

            result.VisitsInHour = new long[24];

            for (int i = 1; i <= 24; i++)
            {
                result.VisitsInHour[i-1] = _mongoCollection.AsQueryable()
                                                .Where(p => p.Time <= TodaysBeginning.AddHours(i) && p.Time >= TodaysBeginning.AddHours(i-1))
                                                .LongCount();
            }

            return result;
        }
    }

    public class TodayVisitsReportDto
    {
        public long VisitsCount { get; set; }
        public long VisitorsCount { get; set; }
        public double VisitsPerVisitor { get; set; }
        public long OnlineVisitorsCount { get; set; }
        public long[] VisitsInHour { get; set; }
    }


}

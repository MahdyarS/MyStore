using MongoDB.Driver;
using MyStore.Application.Interfaces.Contexts;
using MyStore.Domain.Entities.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStore.Common.Services.SetPrecision;

namespace MyStore.Application.Services.VisitorServices.GetLastMonthVisitorsReportService
{
    public interface IGetLastMonthVisitorsReportService
    {
        LastMonthVisitsReportDto Execute();
    }

    public class GetLastMonthVisitorsReportService : IGetLastMonthVisitorsReportService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> _mongoCollection;

        public GetLastMonthVisitorsReportService(IMongoDbContext<Visitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _mongoCollection = _mongoDbContext.GetCollections();
        }

        public LastMonthVisitsReportDto Execute()
        {
            var result = new LastMonthVisitsReportDto();

            var LastMonthsBeginning = DateTime.Now.Date.AddMonths(-1).AddDays(1);
            var LastMonthsEnd = DateTime.Now.Date;

            result.VisitsCount = _mongoCollection.AsQueryable()
                                        .Where(p => p.Time >= LastMonthsBeginning)
                                        .LongCount();

            result.VisitorsCount = _mongoCollection.AsQueryable()
                                        .Where(p => p.Time >= LastMonthsBeginning)
                                        .GroupBy(p => p.VisitorId)
                                        .LongCount();

            if (result.VisitorsCount == 0)
                result.VisitsPerVisitor = 0;
            else
                result.VisitsPerVisitor = ((double)result.VisitsCount) / result.VisitorsCount;

            result.VisitsPerVisitor = result.VisitsPerVisitor.SetPrecision(2);

            result.AvgVisitsInHour = new long[24];

            long[,] avgVisitsInHourInEachDay = new long[30,24];

            int ii = 0;
            for (DateTime i = LastMonthsBeginning; i < LastMonthsEnd; i=i.AddDays(1))
            {
                int jj = 0;
                for (DateTime j = i; j < i.AddDays(1); j=j.AddHours(1))
                {
                    avgVisitsInHourInEachDay[ii,jj] = _mongoCollection.AsQueryable().Where(p => p.Time >= j && p.Time < j.AddHours(1)).LongCount();
                    jj++;
                }
                ii++;
            }


            for (int i = 0; i < 24; i++)
            {
                long sum = 0;
                for (int j = 0; j < 30; j++)
                {
                    sum += avgVisitsInHourInEachDay[j,i];
                }
                result.AvgVisitsInHour[i] = sum / 30;
            }


            return result;
        }
    }

    public class LastMonthVisitsReportDto
    {
        public long VisitsCount { get; set; }
        public long VisitorsCount { get; set; }
        public double VisitsPerVisitor { get; set; }
        public long[] AvgVisitsInHour { get; set; }
    }
}

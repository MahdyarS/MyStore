using MongoDB.Driver;
using MyStore.Application.Interfaces.Contexts;
using MyStore.Common.Services.SetPrecision;
using MyStore.Domain.Entities.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.VisitorServices.GetTotalVisitorsReportService
{
    public interface IGetTotalVisitorsReportService
    {
        TotalVisitsReportDto Execute();
    }

    public class GetTotalVisitorsReportService : IGetTotalVisitorsReportService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> _mongoCollection;

        public GetTotalVisitorsReportService(IMongoDbContext<Visitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _mongoCollection = _mongoDbContext.GetCollections();
        }

        public TotalVisitsReportDto Execute()
        {
            var result = new TotalVisitsReportDto();

            result.VisitsCount = _mongoCollection.AsQueryable().LongCount();
            result.VisitorsCount = _mongoCollection.AsQueryable().GroupBy(p => p.VisitorId).LongCount();
            result.VisitsPerVisitor = ((double)result.VisitsCount) / result.VisitorsCount;

            result.VisitsPerVisitor = result.VisitsPerVisitor.SetPrecision(2);

            return result;

        }
    }

    public class TotalVisitsReportDto
    {
        public long VisitsCount { get; set; }
        public long VisitorsCount { get; set; }
        public double VisitsPerVisitor { get; set; }
    }
}

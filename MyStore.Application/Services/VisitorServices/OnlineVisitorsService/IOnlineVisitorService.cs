using MongoDB.Driver;
using MyStore.Application.Interfaces.Contexts;
using MyStore.Domain.Entities.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.VisitorServices.OnlineVisitorsService
{
    public interface IOnlineVisitorService
    {
        void ConnectNewVisitor(string clientId);
        void DisConnectVisitor(string clientId);
        long GetOnlineUsersCount();
    }
    public class OnlineVisitorService : IOnlineVisitorService
    {
        private readonly IMongoDbContext<OnlineVisitor> _mongoDbContext;
        private readonly IMongoCollection<OnlineVisitor> _mongoCollection;

        public OnlineVisitorService(IMongoDbContext<OnlineVisitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _mongoCollection = _mongoDbContext.GetCollections();
        }

        public void ConnectNewVisitor(string clientId)
        {
            _mongoCollection.InsertOne(new OnlineVisitor()
            {
                ClientId = clientId,
                Time = DateTime.Now
            });
        }

        public void DisConnectVisitor(string clientId)
        {
            _mongoCollection.FindOneAndDelete(p => p.ClientId == clientId);
        }

        public long GetOnlineUsersCount()
        {
            return _mongoCollection.AsQueryable().LongCount();
        }
    }


}

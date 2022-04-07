using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using System.Text;
using System.Threading.Tasks;
using MyStore.Application.Interfaces.Contexts;

namespace MyStore.Persistence.Contexts.MongoDbContext
{
    public class MongoDbContext<T> : IMongoDbContext<T>
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoDbContext(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            _mongoCollection = _mongoClient.GetDatabase("VisitorsDb").GetCollection<T>(typeof(T).Name);
        }

        public IMongoCollection<T> GetCollections()
        {
            return _mongoCollection;
        }
    }
}

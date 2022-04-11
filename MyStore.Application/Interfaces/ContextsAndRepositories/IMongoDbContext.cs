using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Interfaces.Contexts
{
    public interface IMongoDbContext<T>
    {
        IMongoCollection<T> GetCollections();
    }
}

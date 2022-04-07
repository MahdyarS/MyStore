using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities.Visitor
{
    public class Visitor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Ip { get; set; }
        public string CurrentLink { get; set; }
        public string RefererLink { get; set; }
        public string Protool { get; set; }
        public string Method { get; set; }
        public string PhysicalPath { get; set; }
        public VisitorsSoftwareInfo Browser { get; set; }
        public VisitorsSoftwareInfo OS { get; set; }
        public VisitorsDevice Device { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Time { get; set; }
        public string VisitorId { get; set; }


    }

    public class VisitorsSoftwareInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    public class VisitorsDevice
    {
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public bool IsSpider { get; set; }
    }

}

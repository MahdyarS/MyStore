using MongoDB.Driver;
using MyStore.Application.Interfaces.Contexts;
using MyStore.Domain.Entities.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.VisitorServices.SaveVisitorInfoService
{
    public interface ISaveVisitorsInfoService
    {
        void Execute(SaveVisitorInfoRequestDto request);
    }
    public class SaveVisitorsInfoService : ISaveVisitorsInfoService
    {
        private readonly IMongoDbContext<Visitor> _context;
        private readonly IMongoCollection<Visitor> _mongoCollection;

        public SaveVisitorsInfoService(IMongoDbContext<Visitor> context)
        {
            _context = context;
            _mongoCollection = _context.GetCollections();
        }

        public void Execute(SaveVisitorInfoRequestDto request)
        {
            _mongoCollection.InsertOne(new Visitor()
            {
                Ip = request.Ip,
                CurrentLink = request.CurrentLink,
                RefererLink = request.RefererLink,
                Protool = request.Protool,
                Method = request.Method,
                PhysicalPath = request.PhysicalPath,
                Browser = new VisitorsSoftwareInfo()
                {
                    Name = request.Browser.Name,
                    Version = request.Browser.Version
                },
                OS = new VisitorsSoftwareInfo()
                {
                    Name = request.OS.Name,
                    Version = request.OS.Version
                },
                Device = new VisitorsDevice()
                {
                    Brand = request.Device.Brand,
                    Name = request.Device.Brand,
                    Model = request.Device.Model,
                    IsSpider = request.Device.IsSpider,
                },
                Time = request.Time,
                VisitorId = request.VisitorId
            });
        }
    }

    public class SaveVisitorInfoRequestDto
    {
        public string Ip { get; set; }
        public string CurrentLink { get; set; }
        public string RefererLink { get; set; }
        public string Protool { get; set; }
        public string Method { get; set; }
        public string PhysicalPath { get; set; }
        public VisitorsSoftwareInfoDto Browser { get; set; }
        public VisitorsSoftwareInfoDto OS { get; set; }
        public VisitorsDeviceDto Device { get; set; }
        public DateTime Time { get; set; }
        public string VisitorId { get; set; }
    }

    public class VisitorsSoftwareInfoDto
    {
        public string Name { get; set; }
        public string Version { get; set; }

    }
    public class VisitorsDeviceDto
    {
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public bool IsSpider { get; set; }
    }

}

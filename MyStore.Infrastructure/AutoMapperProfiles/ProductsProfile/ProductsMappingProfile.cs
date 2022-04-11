using AutoMapper;
using MyStore.Application.Services.ProductsServices.Command.AddNewCategoryService;
using MyStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.AutoMapperConfigurations.ProductsProfile
{
    public class ProductsMappingProfile:Profile
    {
        public ProductsMappingProfile()
        {
            CreateMap<AddCategoryRequestDto, CategoryToAdd>();
        }
    }
}

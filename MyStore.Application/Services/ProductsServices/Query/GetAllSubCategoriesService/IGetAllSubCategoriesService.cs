using AutoMapper;
using MyStore.Application.Interfaces.ContextsAndRepositories;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.ProductsServices.Query.GetAllSubCategoriesService
{
    public interface IGetAllSubCategoriesService
    {
        Task<ResultDto<List<CategoryAbstract>>> Execute(int parentId);
    }

    public class GetAllSubCategoriesService : IGetAllSubCategoriesService
    {
        private readonly ICategoriesRepositoryInterface categoriesRepository;
        private IMapper mapper;
        public GetAllSubCategoriesService(ICategoriesRepositoryInterface categoriesRepository, IMapper mapper)
        {
            this.categoriesRepository = categoriesRepository;
            this.mapper = mapper;
        }
        public async Task<ResultDto<List<CategoryAbstract>>> Execute(int parentId)
        {
            try
            {
                var parent = await categoriesRepository.GetCategoryById(parentId);
                var subs = await categoriesRepository.GetSubCategories(parentId);
                

                if(parent == null && parentId == 0)
                    return new ResultDto<List<CategoryAbstract>>(true, "اصلی", subs.ToList());

                if (parent == null)
                    return new ResultDto<List<CategoryAbstract>>(false, "گروه بالا دستی معتبر نیست!");


                return new ResultDto<List<CategoryAbstract>>(true, parent.CategoryName, subs.ToList());
            }
            catch (Exception)
            {
                return new ResultDto<List<CategoryAbstract>>(false, "خطایی هنگام عملیات رخ داد!");
            }
}
    }
}

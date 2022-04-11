using AutoMapper;
using MyStore.Application.Interfaces.ContextsAndRepositories;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.ProductsServices.Command.AddNewCategoryService
{
    public interface IAddNewCategoryService
    {
        Task<ResultDto> Execute(AddCategoryRequestDto category);
    }

    public class AddNewCategoryService : IAddNewCategoryService
    {
        private readonly ICategoriesRepositoryInterface _categoriesRepository;
        private IMapper mapper;

        public AddNewCategoryService(ICategoriesRepositoryInterface categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            this.mapper = mapper;
        }

        public async Task<ResultDto> Execute(AddCategoryRequestDto category)
        {
            try
            {
                if (!category.ParentId.HasValue)
                {
                    var toAdd = mapper.Map<CategoryToAdd>(category);
                    await _categoriesRepository.AddNewCategory(toAdd);
                    return new ResultDto(true, "گروه با موفقیت اضافه شد!");
                }
                var parent = await _categoriesRepository.GetCategoryById(category.ParentId.Value);
                if (parent == null)
                    return new ResultDto(false, "گروه بالایی وجود ندارد");
                if (parent.ProductsCount != 0)
                    return new ResultDto(false, "امکان اضافه کردن زیرگروه به کروهی که در آن کالا وجود دارد نمی باشد!");
                await _categoriesRepository.AddNewCategory(mapper.Map<CategoryToAdd>(category));
                return new ResultDto(true, "گروه با موفقیت اضافه شد!");

            }
            catch (Exception)
            {
                return new ResultDto(false, "خطایی هنگام عملیات رخ داد!");
            }
        }
    }

    public class AddCategoryRequestDto
    {
        public int? ParentId { get; set; } = null;
        public string CategoryName { get; set; }
    }

}

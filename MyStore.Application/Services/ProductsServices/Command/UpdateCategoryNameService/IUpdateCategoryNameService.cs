using MyStore.Application.Interfaces.ContextsAndRepositories;
using MyStore.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.ProductsServices.Command.UpdateCategoryNameService
{
    public interface IUpdateCategoryNameService
    {
        Task<ResultDto> Execute(UpdateCategoryNameRequestDto request);
    }

    public class UpdateCategoryNameService : IUpdateCategoryNameService
    {
        ICategoriesRepositoryInterface categoriesRepository;

        public UpdateCategoryNameService(ICategoriesRepositoryInterface categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<ResultDto> Execute(UpdateCategoryNameRequestDto request)
        {
            try
            {
                await categoriesRepository.UpdateCategoryName(request.Id,request.NewName);
                return new ResultDto(true, $"نام گروه با موفقیت به ({request.NewName}) تغییر یافت!");
            }
            catch (Exception)
            {
                return new ResultDto(false, "خطایی هنگام عملیات رخ داد!");
            }
        }
    }

    public class UpdateCategoryNameRequestDto
    {
        public string NewName { get; set; }
        public int Id { get; set; }
    }
}

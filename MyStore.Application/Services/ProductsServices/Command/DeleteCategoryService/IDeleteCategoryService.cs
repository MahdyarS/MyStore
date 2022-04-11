using MyStore.Application.Interfaces.ContextsAndRepositories;
using MyStore.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.ProductsServices.Command.DeleteCategoryService
{
    public interface IDeleteCategoryService
    {
        Task<ResultDto> Execute(int id);
    }
    public class DeleteCategoryService : IDeleteCategoryService
    {
        ICategoriesRepositoryInterface categoriesRepository;

        public DeleteCategoryService(ICategoriesRepositoryInterface categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<ResultDto> Execute(int id)
        {
            var toDelete = await categoriesRepository.GetCategoryById(id);
            if (toDelete == null)
            {
                return new ResultDto(false, "گروه مورد نظر در لیست وجود ندارد!");
            }
            if(toDelete.ProductsCount + toDelete.SubCategoriesCount == 0)
            {
                await categoriesRepository.DeleteCategory(id);
                return new ResultDto(true, "گروه با موفقیت حذف شد!");
            }
            return new ResultDto(false, "گروه قابل حذف نمی باشد!");
        }
    }
}

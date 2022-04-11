using MyStore.Application.Interfaces.ContextsAndRepositories;
using MyStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.ProductsServices.Query.GetCategoryById
{
    public interface IGetCategoryById
    {
        Task<Category> Execute(int id);
    }

    public class GetCategoryById : IGetCategoryById
    {
        private readonly ICategoriesRepositoryInterface categoriesRepository;

        public GetCategoryById(ICategoriesRepositoryInterface categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<Category> Execute(int id)
        {
            var result = await categoriesRepository.GetCategoryById(id);

            if (id == 0 && result == null)
                return new Category() { CategoryName = "Root" };

            return result;
        }
    }
}

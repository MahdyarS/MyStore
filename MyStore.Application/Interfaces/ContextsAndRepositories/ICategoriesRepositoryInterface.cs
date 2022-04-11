using MyStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Interfaces.ContextsAndRepositories
{
    public interface ICategoriesRepositoryInterface
    {
        //add    : input(parentId,Name)             , output -----> id or null
        //get    : input(parentId)                  , output -----> id and Name
        //get    : input(id)                        , output -----> * maybe except Name
        //get    : input()                          , output -----> * maybe except Name or just names
        //update : input(id)                        , output -----> id or null
        //delete : input(id)                        , output -----> id or null
        Task<int> AddNewCategory(CategoryToAdd category);
        Task<Category> GetCategoryById(int id);   //  full get just used when we wanna add to its subcategories or products
        Task<List<CategoryAbstract>> GetSubCategories(int? parentId);
        Task UpdateCategoryName(int id, string newName);
        Task DeleteCategory(int id);
    }
}


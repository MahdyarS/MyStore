using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MyStore.Application.Interfaces.ContextsAndRepositories;

namespace MyStore.Persistence.ContextsAndRepositories.CategoriesRepository
{
    public interface ICategoriesRepository
    {
        //add    : input(parentId,Name)             , output -----> id or null
        //get    : input(parentId)                  , output -----> id and Name
        //get    : input(id)                        , output -----> * maybe except Name
        //get    : input()                          , output -----> * maybe except Name or just names
        //update : input(id)                        , output -----> id or null
        //delete : input(id)                        , output -----> id or null
        Task<int> AddNewCategory(CategoryToAdd category);
        Task<Category> GetCategoryById(int id);   //full get just used when we wanna add to its subcategories or products
        Task<List<CategoryAbstract>> GetSubCategories(int? parentId);
        Task UpdateCategoryName(int id, string newName);
        Task DeleteCategory(int id);
    }

    public class CategoriesRepository : ICategoriesRepository, ICategoriesRepositoryInterface
    {
        private readonly IConfiguration Configuration;
        private string connectionString;
        public CategoriesRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddNewCategory(CategoryToAdd category)
        {
            string query = @"SP_Categories_Insert";

            var parameters = new DynamicParameters();
            parameters.Add("ParentId", category.ParentId);
            parameters.Add("CategoryName", category.CategoryName);
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                int id = parameters.Get<int>("Id");
                return id;
            }
        }

        public async Task DeleteCategory(int id)
        {
            string query = @"SP_Categories_Delete";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, new { Id = id }, commandType: CommandType.StoredProcedure);
                return;
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            string query = @"SP_Categories_GetCategoryById";
            using (var connection = new SqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<Category>(query, new { Id = id }, commandType: CommandType.StoredProcedure);
                return result.SingleOrDefault();
            }
        }

        public async Task<List<CategoryAbstract>> GetSubCategories(int? parentId)
        {
            string query = @"SP_Categories_GetAbstractSubCategories";
            using (var connection = new SqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<CategoryAbstract>(query, new { ParentId = parentId },commandType:CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task UpdateCategoryName(int id, string newName)
        {
            string query = @"SP_Categories_UpdateCategoryName";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, new { Id = id, Name = newName }, commandType: CommandType.StoredProcedure);
                return;
            }
        }
    }
}

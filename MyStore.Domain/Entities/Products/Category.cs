using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities.Products
{
    public class Category
    {
        public int Id { get; set; }
        public int ParentId { get; set; } = 0;
        public string CategoryName { get; set; }
        public int ProductsCount { get; set; } = 0;
        public byte SubCategoriesCount { get; set; } = 0;
    }

    public class CategoryToAdd
    {

        public CategoryToAdd(string categoryName, int parentId = 0)
        {
            CategoryName = categoryName;
            ParentId = parentId;
        }
        public string CategoryName { get; set; }

        public int? ParentId { get; set; }
    }

    public class CategoryAbstract
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
    }

}

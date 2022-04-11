using System;
using MyStore.Persistence.ContextsAndRepositories.CategoriesRepository;


namespace TestApp
{
    public enum Name
    {
        Mahdyar
    }

    class Program
    {
        static void Main(string[] args)
        {

            //ICategoriesRepository categoriesRepository = new CategoryRepository();




            Console.ReadKey();
        }
    }

    public enum RoleName : byte
    {
        Admin,
        Operator,
        User = 100
    }



}

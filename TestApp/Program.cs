using System;

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
            Console.WriteLine();

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

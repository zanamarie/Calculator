using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcIt
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to CalcIt app. To exit CalcIt just type exit ");

            while (true)
            {
                var result = Parser.CalculateExpression("Enter experssion:");
                Console.WriteLine(result);
                Console.ReadKey();
            }
        }
    }
}

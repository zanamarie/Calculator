using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcIt
{
    class Program
    {
        public static string AskAndProcessUsersAnswer(string requirementStatement)
        {
            Console.WriteLine(requirementStatement);

            var inputValue = Console.ReadLine();

            if (inputValue.Contains("exit"))
                Environment.Exit(0);

            return inputValue;
        }

        public static double AskUserForNumericValue(string requirementStatement)
        {
            double parsedInputNumericValue;
            do
            {
                var inputNumericValue = AskAndProcessUsersAnswer(requirementStatement);
                if (!Double.TryParse(inputNumericValue, out parsedInputNumericValue))
                {
                    Console.WriteLine("String that you entered contains nonnumeric characters!");
                }
                else break;
            }
            while (true);

            return parsedInputNumericValue;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to CalcIt app. To exit CalcIt just type exit ");

            while (true)
            {
                Processor calculation = new Processor();

                var parsedFirstOperator = AskUserForNumericValue("Enter first number");
                var parsedSecondOperator = AskUserForNumericValue("Enter second number");
                var operation = AskAndProcessUsersAnswer("Enter operation between previously entered numbers");
                var result = calculation.BinaryOperations(operation, parsedFirstOperator, parsedSecondOperator);

                Console.WriteLine(parsedFirstOperator + operation + parsedSecondOperator + "=" + result);
                Console.ReadKey();
            }
        }
    }
}

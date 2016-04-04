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
        static int tokenPosition;
        static string token;
        static string expression;
        static Stack<double> operands;
        static Stack<string> operators;
        public const string Add = "+", Subtract = "-", Multiply = "*", Divide = "/";
        public static string[] binaryOperators = new string[] { Multiply, Divide, Subtract, Add };

        private static void ResetCalculator()
        {
            operands = new Stack<double>();
            operators = new Stack<string>();
            operators.Push("$");
            tokenPosition = -1;
            token = String.Empty;
        }


        public static double CalculateExpression(string requirementStatement)
        {
            ResetCalculator();

            expression = CheckIfUserWantsToExit(requirementStatement);

            if (NormalizeExpression(ref expression))
            {
                return Parse();
            }
            else
            {
                throw new Exception();
            }

        }
        public static double Parse()
        {

            ParseNumericValue();
            while (CheckIfTokenIsBinaryOperator(token))
            {
                PushOrCalculateOperator();
                ParseNumericValue();
            }

            while (operators.Peek() != "$")
            {

                double secondOperand = operands.Pop();
                double firstOperand = operands.Pop();
                CalculateBinaryOperation(operators.Pop(), firstOperand, secondOperand);
            }

            return operands.Pop();
        }

        public static void ParseNumericValue()
        {
            GetNextToken();
            if (CheckIfTokenIsDigit(token))
                ParseDigit();
        }

        public static void PushOrCalculateOperator()
        {
            while (CheckOperatorPriority(operators.Peek()) >= CheckOperatorPriority(token))
            {
                    double secondOperand = operands.Pop();
                    double firstOperand = operands.Pop();
                    CalculateBinaryOperation(operators.Pop(), firstOperand, secondOperand);
            }
            operators.Push(token);
        }

        public static void CalculateBinaryOperation(string operation, double operand1, double operand2)
        {
            switch (operation)
            {
                case "+":
                    operands.Push(operand1 + operand2); break;
                case "-":
                    operands.Push(operand1 - operand2); break;
                case "*":
                    operands.Push(operand1 * operand2); break;
                case "/":
                    operands.Push(operand1 / operand2); break;
                default:
                    throw new Exception();
            }
        }
        public static bool CheckIfTokenIsDigit(string token)
        {
            return Regex.IsMatch(token.ToString(), @"^[0-9]+$");
        }

        public static bool CheckIfTokenIsBinaryOperator(string token)
        {
            if (Array.IndexOf(binaryOperators, token) != -1)
                return true;
            else
                return false;
        }
        public static void GetNextToken()
        {
            if (token != "$")
                token = expression[++tokenPosition].ToString();
        }
        public static void ParseDigit()
        {
            StringBuilder number = new StringBuilder();

            while (CheckIfTokenIsDigit(token))
            {
                number.Append(token);
                GetNextToken();
            }

            operands.Push(double.Parse(number.ToString()));
        }
        public static string CheckIfUserWantsToExit(string requirementStatement)
        {
            Console.WriteLine(requirementStatement);

            var inputValue = Console.ReadLine();

            if (inputValue.Contains("exit"))
                Environment.Exit(0);

            return inputValue;
        }
        public static bool NormalizeExpression(ref string expression)
        {
            if (expression.Length > 2)
            {
                expression = expression.Replace(" ", "").Replace("\t", "")+"$";
                CheckIfUserEnteredValidExpression(expression);
                return true;
            }
            else
                return false;
            
        }
        public static void CheckIfUserEnteredValidExpression(string expression)
        {

            var tempSavedToken = expression[0];

            for (int i = 1; i < expression.Length; i++)
            {
                if (!CheckIfTokenIsDigit(expression[i].ToString()) && !CheckIfTokenIsDigit(tempSavedToken.ToString()))
                {
                    Console.WriteLine("You haven't entered valid mathematic expression! Exiting Calcit... ");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                tempSavedToken = expression[i];
            }
        }

        private static int CheckOperatorPriority(string operatorToBeChecked)
        {
            switch (operatorToBeChecked)
            {
                case Subtract: return 1;
                case Add: return 1;
                case Multiply: return 2;
                case Divide: return 2;
                default: return 0;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to CalcIt app. To exit CalcIt just type exit ");

            while (true)
            {
         

                Processor calculation = new Processor();
              
                var result = CalculateExpression("Enter experssion:");
                Console.WriteLine(result);
                Console.ReadKey();
            }
        }
    }
}

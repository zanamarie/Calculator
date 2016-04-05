using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcIt
{
    public static class Parser
    {
        static string expression;
        public static Stack<double> operands;
        public static Stack<string> operators;
        static string token;

        private static void ResetCalculator()
        {
            operands = new Stack<double>();
            operators = new Stack<string>();
            operators.Push(Token.StartEndToken);
            Token.tokenPosition = -1;
            token = Token.NullToken;
        }

        public static double CalculateExpression(string requirementStatement)
        {
            ResetCalculator();
            expression = CheckIfUserWantsToExit(requirementStatement);
            if (NormalizeExpression(ref expression))
                return ParseExpression();
            else
                throw new Exception();
        }

        public static bool NormalizeExpression(ref string expression)
        {
            if (expression.Length > 2)
            {
                expression = expression.Replace(" ", "").Replace("\t", "") + Token.StartEndToken;
                return true;
            }
            else
                return false;
        }

        public static double ParseExpression()
        {
            ParseExpressionInTokenLevel();

            while (CheckIfTokenIsBinaryOperator(token))
            {
                PushOperatorOrDoBinaryAction();
                ParseExpressionInTokenLevel();
            }
            while (operators.Peek() != Token.StartEndToken)
                PopOperatorAndDoBinaryAction();

            return operands.Peek();
        }

        public static void ParseExpressionInTokenLevel()
        {
            GetNextToken();

            if (CheckIfTokenIsDigit(token))
                ParseDigit();
            else if (token == Token.LeftBracket)
            {
                operators.Push(Token.StartEndToken);
                ParseExpression();
                if (token == Token.RightBracket)
                    operators.Pop();
                else  
                    throw new Exception();
            }
            else
                throw new Exception();
        }

        public static void PushOperatorOrDoBinaryAction()
        {
            while (CheckOperatorPriority(operators.Peek()) >= CheckOperatorPriority(token))
            {
                PopOperatorAndDoBinaryAction();
            }
            operators.Push(token);
        }

        public static void PopOperatorAndDoBinaryAction()
        {
            double secondOperand = operands.Pop();
            double firstOperand = operands.Pop();
            Processor.CalculateBinaryOperation(operators.Pop(), firstOperand, secondOperand, ref operands);
        }

        public static bool CheckIfTokenIsDigit(string token)
        {
            return Regex.IsMatch(token.ToString(), @"^[0-9]+$");
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

        public static bool CheckIfTokenIsBinaryOperator(string token)
        {
            return Array.IndexOf(Token.binaryOperators, token) != -1;
        }

        public static void GetNextToken()
        {
            if (token != "$")
                token = expression[++Token.tokenPosition].ToString();
        }

        private static int CheckOperatorPriority(string operatorToBeChecked)
        {
            switch (operatorToBeChecked)
            {
                case Token.Add: return 1;
                case Token.Subtract: return 1;
                case Token.Multiply: return 2;
                case Token.Divide: return 2;
                case Token.LeftBracket: return 3;
                case Token.RightBracket: return 3;
                default: return 0;
            }
        }

        public static string CheckIfUserWantsToExit(string requirementStatement)
        {
            Console.WriteLine(requirementStatement);
            var inputValue = Console.ReadLine();

            if (inputValue.Contains("exit"))
                Environment.Exit(0);

            return inputValue;
        }
    }
}

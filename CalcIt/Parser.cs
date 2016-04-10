using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcIt
{
    public partial class Calculator
    {
        public static string expression;
        public static Stack<double> operands;
        public static Stack<string> operators;
        public static int tokenPosition;
        public static string token;
        public Calculator()
        {
            operands = new Stack<double>();
            operators = new Stack<string>();
            operators.Push(Token.StartEndToken);
            tokenPosition = -1;
            token = Token.NullToken;
        }

        public double CalculateExpression(string expr)
        {
            expression = expr;
            CheckIfUserWantsToExit(expression);
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

            while (Token.CheckIfTokenIsBinaryOperator(token))
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

            if (Token.CheckIfTokenIsDigit(token))
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
            while (CheckBinaryTokenPriority(operators.Peek()) >= CheckBinaryTokenPriority(token))
            {
                PopOperatorAndDoBinaryAction();
            }
            operators.Push(token);
        }

        public static void PopOperatorAndDoBinaryAction()
        {
            double secondOperand = operands.Pop();
            double firstOperand = operands.Pop();
            CalculateBinaryOperation(operators.Pop(), firstOperand, secondOperand, ref operands);
        }

        public static void GetNextToken()
        {
            if (token != Token.StartEndToken)
                token = expression[++tokenPosition].ToString();
        }

        public static void ParseDigit()
        {
            StringBuilder number = new StringBuilder();

            while (Token.CheckIfTokenIsDigit(token))
            {
                number.Append(token);
                GetNextToken();
            }
            operands.Push(double.Parse(number.ToString()));
        }

        public static void CheckIfUserWantsToExit(string expression)
        {
            if (expression.Contains("exit"))
                Environment.Exit(0);
        }
    }
}

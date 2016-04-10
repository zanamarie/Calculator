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
        public static class Token
        {
            public const string Add = "+", Subtract = "-", Multiply = "*", Divide = "/", StartEndToken = "$", NullToken = "", LeftBracket = "(", RightBracket = ")";
            public static string[] binaryOperators = new string[] { Multiply, Divide, Subtract, Add };

            public static bool CheckIfTokenIsDigit(string token)
            {
                return Regex.IsMatch(token.ToString(), @"^[0-9]+$");
            }

            public static bool CheckIfTokenIsBinaryOperator(string token)
            {
                return Array.IndexOf(Token.binaryOperators, token) != -1;
            }
 
            private static int CheckBinaryTokenPriority(string operatorToBeChecked)
            {
                switch (operatorToBeChecked)
                {
                    case Add: return 1;
                    case Subtract: return 1;
                    case Multiply: return 2;
                    case Divide: return 2;
                    case LeftBracket: return 3;
                    case RightBracket: return 3;
                    default: return 0;
                }
            }
        }
    }
}

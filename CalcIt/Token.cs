using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcIt
{
    public static class Token
    {
        public static int tokenPosition;
        public static string token;
        public const string Add = "+", Subtract = "-", Multiply = "*", Divide = "/", StartEndToken = "$", NullToken = "", LeftBracket = "(", RightBracket = ")";
        public static string[] binaryOperators = new string[] { Multiply, Divide, Subtract, Add };
    }
}

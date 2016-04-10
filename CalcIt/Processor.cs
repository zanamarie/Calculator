using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcIt
{
    public partial class Calculator
    { 
        public static void CalculateBinaryOperation(string operation, double operand1, double operand2,ref Stack<double> operands)
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
    }
}

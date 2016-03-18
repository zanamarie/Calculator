using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcIt
{
    public class Processor
    {
        public double BinaryOperations(string operation,double operator1,double operator2)
        {
            switch (operation)
            {
                case "+":
                    return operator1 + operator2;
                case "-":
                    return operator1 - operator2;
                case "*":
                    return operator1 * operator2;
                case "/":
                    return operator1 / operator2;
                default:

                   throw new Exception();
            }
        }
    }
}

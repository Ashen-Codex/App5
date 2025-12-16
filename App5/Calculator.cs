using System;
using System.Collections.Generic;
using System.Text;

namespace App5
{
    public class Calculator
    {
        private double firstNumber;
        private double secondNumber;
        private string operation;

        public double Calculate(double num1, double num2, string op)
        {
            if (op == "+")
                return num1 + num2;

            if (op == "-")
                return num1 - num2;

            if (op == "*")
                return num1 * num2;

            if (op == "/")
            {
                if (num2 == 0)
                    throw new System.DivideByZeroException();
                return num1 / num2;
            }

            throw new System.ArgumentException("Неизвестная операция");
        }
    }
    
}


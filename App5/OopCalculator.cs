using System;
using System.Collections.Generic;
using System.Text;

namespace App5
{
    public class OopCalculator
    {
        private int _sum;

        public int Calculate(int n)
        {
            _sum = 0;
            for (int i = 1; i <= n; i++)
            {
                _sum = AddSquare(_sum, i);
            }
            return _sum;
        }

        private int AddSquare(int current, int number) => current + (number * number);
    }
}

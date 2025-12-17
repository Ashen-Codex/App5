using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace App5
{
    public static class FunctionalCalculator
    {
        public static int SumOfSquares(int n)
        {
            return Enumerable.Range(1, n)
                            .Select(x => x * x)
                            .Aggregate(0, (acc, x) => acc + x);
        }
    }
}   

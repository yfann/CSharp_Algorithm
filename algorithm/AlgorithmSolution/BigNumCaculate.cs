using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace AlgorithmSolution
{
    public class BigNumCaculate
    {
        public static void Caculate()
        {
            BigIntegerTest();
        }
        public static void BigIntegerTest()
        {
            BigInteger result = 1;

            for (int i = 1; i <= 100; i++)
            {
                result *= i;
            }

            Console.WriteLine("100! bigInterger result:"+result);
            Console.WriteLine("2^64:"+BigInteger.Pow(2,16));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmSolution
{
    public class Fibonaccic 
    {
        public static void Caculate(int top)
        {
            Print(Fibo(top));
        }

        public static int Fibo(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            else if (n == 1)
            {
                return 1;
            }
            else
            {
                int temp = Fibo(n - 2) + Fibo(n - 1);
                return temp;
            }
        }

        public static void Print(int n)
        {
            Console.Write(string.Format("{0}  ",n));
        }

        public static void Caculate2(int top)
        {
            Fibo2(top);
        }
        public static int Fibo2(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            else if (n == 1)
            {
                return 1;
            }

            int firstNum = 0;
            int secondNum = 1;
            int result = 0;
            int i = 1;
            while (i<n)
            {
                result = firstNum + secondNum;
                firstNum = secondNum;
                secondNum = result;
                ++i;
                Print(result);
            }
            return result;

        }

    }
}

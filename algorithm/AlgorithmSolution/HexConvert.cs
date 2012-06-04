using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmSolution
{
    public class HexConvert
    {
        public static void Caculate()
        {
            TenToTwo();
        }

        public static void TenToTwo()
        {
            Console.WriteLine("请输入整数：");
            string str=Console.ReadLine();
            int a = 0;
            int.TryParse(str,out a);

            int ao=a;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            while (true)
            {
                int t = a % 2;
                sb.Append(t);
                a = a / 2;
                if (a < 2)
                {
                    sb.Append(a);
                    break;
                }

            }
            char[] ch=sb.ToString().ToArray();
            for (int i = ch.Length - 1; i >= 0; i--)
            {
                sb2.Append(ch[i]);
            }

                Console.WriteLine(sb2.ToString());
            Console.WriteLine(Convert.ToString(ao,2));

        }

        public static void TwoToTen()
        {

        }
    }
}

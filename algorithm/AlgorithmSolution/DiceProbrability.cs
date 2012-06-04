using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmSolution
{
    public class DiceProbrability
    {
        public static int DiceMax = 6;
        public static void Caculate(int diceNum)
        {
            int maxSum=DiceMax*diceNum;
            float[] prop=new float[maxSum-diceNum+1];
            for (int s = diceNum; s <= maxSum; s++)
            {
                prop[s - diceNum] = GetChanceNum(s,diceNum);
            }

            int total = (int)Math.Pow(DiceMax,diceNum);

            for (int j = 0; j < prop.Length; j++)
            {
                prop[j]/=total;
                Console.WriteLine(string.Format("{0}-----{1}%",diceNum+j,prop[j]*100));
            }
        }
        public static int GetChanceNum(int Total,int diceNum)//骰子点数总和，骰子个数，返回出现个数
        {
            if(diceNum==1)
            {
                if (Total >= 1 && Total <= DiceMax * diceNum)
                    return 1;
                else
                    return 0;
            }
            else if (diceNum > 1)
            {
                int sum = 0;
                for (int i = 1; i <= DiceMax; i++)
                {
                    sum += GetChanceNum(Total-i,diceNum-1);
                }
                return sum;
            }
            else
            {
                return 0;
            }

        }

    }
}

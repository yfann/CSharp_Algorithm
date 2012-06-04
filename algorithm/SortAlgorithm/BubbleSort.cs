using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgorithm
{
    public class BubbleSort
    {
        //O(n^2) 
        //稳定
        public static void Sort(int[] arr)
        {
            for (int j = arr.Length - 1; j > 0; j--)
            {
                for (int i = 0; i <j; i++)
                {
                        if (arr[i] > arr[i + 1])
                        {
                            int temp = arr[i + 1];
                            arr[i + 1] = arr[i];
                            arr[i] = temp;
                        }
                }
            }
        }
    }
}

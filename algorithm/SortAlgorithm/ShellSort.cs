using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgorithm
{
    public class ShellSort
    {
        //O(n^2)
        //不稳定
        public static void Sort(int[] arr)
        {
            //gap 的选择
            for (int gap = arr.Length / 2; gap > 0; gap /= 2)
            {
                for(int i=gap;i<arr.Length;i++)
                {
                    if (arr[i] < arr[i - gap])
                    {
                        int temp=arr[i];
                        int j = i - gap;
                        while (j >= 0 && arr[j] > temp)
                        {
                            arr[j + gap] = arr[j];
                            j -= gap;
                        }
                        arr[j+gap] = temp;
                    }
                }
            }
        }

    }
}

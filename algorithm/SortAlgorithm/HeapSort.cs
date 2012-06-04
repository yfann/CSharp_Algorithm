using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgorithm
{
    public class HeapSort
    {
        //O(nlog(n))
        //不稳定
        public static void Sort(int[] arr)
        {
            for (int i = arr.Length / 2-1; i >= 0; i--)
            {
                HeapAdjust(arr, i, arr.Length-1);
            }
            for (int i = arr.Length - 1; i > 0; i--)
            {
                Swap(arr,0,i);
                HeapAdjust(arr,0,i-1);
            }
        }
        public static void HeapAdjust(int[] arr, int l, int h)
        {
            int temp = arr[l];
            for (int i = (2 * l + 1); i <= h; )
            {
                if (i < h && arr[i] < arr[i + 1])
                    ++i;
                if(temp>=arr[i])
                    break;

                arr[l] = arr[i];
                l = i;//保存空缺位置

                i = 2 * i + 1;
            }
            arr[l] = temp;
        }
        public static void Swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }
}

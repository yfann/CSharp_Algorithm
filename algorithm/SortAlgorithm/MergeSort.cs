using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgorithm
{
    public class MergeSort
    {
        //O(nlog(n))
        //稳定
        public static void Sort(int[] arr)
        {
            MSort(arr,arr,0,arr.Length-1);
        }
        public static void MSort(int[] arr,int[] arrs,int l,int h)
        {
            int[] temp=new int[arr.Length];
            if (l == h)
            {
                arrs[l] = arr[l];
            }
            else
            {
                int m = (l + h) / 2;
                MSort(arr,temp,l,m);
                MSort(arr,temp,m+1,h);
                Merg(temp,arrs,l,m,h);
            }
        }
        public static void Merg(int[] temp,int[] arrs,int l,int m,int h)
        {
            int i, k;
            for (i = l, k = m + 1; l<=m&&k<=h; i++)
            {
                if (temp[l] < temp[k])
                {
                    arrs[i] = temp[l++];
                }
                else
                {
                    arrs[i] = temp[k++];
                }
            }
            if (l <= m)
            {
                for (int a = 0; a <= (m - l); a++)
                {
                    arrs[i + a] = temp[l + a];
                }
            }
            if (k <= h)
            {
                for (int a = 0; a <= (h - k); a++)
                {
                    arrs[i+a]=temp[k+a];
                }
            }
        }
    }
}
